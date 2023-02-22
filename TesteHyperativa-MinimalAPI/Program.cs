using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using TesteHyperativa_CrossCutting;
using TesteHyperativa_MinimalAPI.Login;
using AutoMapper;
using TesteHyperativa_Repository.Context;
using Microsoft.EntityFrameworkCore;
using TesteHyperativa_MinimalAPI.Mapper;
using TesteHyperativa_MinimalAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TesteHyperativa_Domain.Interfaces.Services;
using TesteHyperativa_Domain.Entities;
using TesteHyperativa_MinimalAPI.Upload;
using TesteHyperativa_Domain.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
var secret = configuration.GetValue<string>("Secret");
var key = Encoding.ASCII.GetBytes(secret);

//Dependency Injection
IoC.Configure(builder.Services);


//Mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//DBCOntext
var connectionString = configuration.GetConnectionString("HyperativaConnectionString");
builder.Services.AddDbContext<HyperativaDBContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly("TesteHyperativa-Repository")));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Hyperativa v1", Description = "Teste Hyperativa com Minimal APIs", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
    c.OperationFilter<FileUploadOperationFilter>();
});

//Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => { 
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
    options.AddPolicy("Employee", policy => policy.RequireRole("employee"));
});
//Login
builder.Services.AddScoped<Login, Login>();

//App

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Hyperativa v1");
});

//app.MapGet("/", () => "API Hyperativa. Favor acessar o Swagger.");
app.MapGet("/", () => Results.Redirect("/swagger/index.html")).WithMetadata(new ApiExplorerSettingsAttribute { IgnoreApi = true });

//Initialize DataBase
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<HyperativaDBContext>();
    DbInitializer.Initialize(dbContext);
};


app.MapPost("/login", (LoginViewModel model) =>
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var LoginService = services.GetRequiredService<Login>();
        return LoginService.GetToken(model);
    }
});

app.MapPost("/CreditCardInclude", async (ClaimsPrincipal user, string creditCardNumber) =>
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var creditCardService = services.GetRequiredService<ICreditCardService>();
        var userId = user.FindFirst(ClaimTypes.Sid)?.Value;
        CreditCard card = new CreditCard();
        card.Number = CryptographyService.Codificar(creditCardNumber);
        card.InputUserId = userId;
        card.InputMode = TesteHyperativa_Domain.Enum.EnumInputMode.OneToOne;
        card.InputDate = DateTime.Now;

        await creditCardService.Add(card);
    }

    Results.Ok(new { message = $"Authenticated as {user.Identity?.Name}" });
}).RequireAuthorization();

app.MapPost("/CreditCardsBatchFile", async (ClaimsPrincipal user, HttpRequest request) =>
{
    var teste = request.Form.Files.Any();
    if (!request.HasFormContentType)
        return Results.BadRequest();

    var form = await request.ReadFormAsync();
    var formFile = form.Files["files"];

    if (formFile ==  null || formFile.Length == 0)
        return Results.BadRequest();

    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var creditCardsBatchFileService = services.GetRequiredService<ICreditCardsBatchFileService>();
        var userId = user.FindFirst(ClaimTypes.Sid)?.Value;

        var fileMetaData = await creditCardsBatchFileService.ProcessFileHeaderData(formFile);

        var fileBodyData = await creditCardsBatchFileService.ProcessFileBodyData(formFile, fileMetaData.AmountOfRecords, userId);


    }

    return Results.Ok();
}).Accepts<IFormFile>("multipart/form-data").Produces(200).RequireAuthorization();


app.Run();
