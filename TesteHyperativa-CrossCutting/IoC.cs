
using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Domain.Interfaces.Services;
using TesteHyperativa_Domain.Services;
using TesteHyperativa_Repository.Context;
using TesteHyperativa_Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TesteHyperativa_CrossCutting
{
    public static class IoC
    {
        public static void Configure(IServiceCollection service)
        {
            #region Registra IOC

            #region IOC Services
            service.AddScoped< IUserService, UserService>();
            service.AddScoped<ICreditCardService, CreditCardService>();
            service.AddScoped<ICreditCardsBatchFileService, CreditCardsBatchFileService>();
            #endregion

            #region IOC Repositorys SQL         
            service.AddScoped<HyperativaDBContext, HyperativaDBContext>();
            service.AddScoped<DbInitializer, DbInitializer>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICreditCardRepository, CreditCardRepository>();
            service.AddScoped<ICreditCardsBatchFileRepository, CreditCardsBatchFileRepository>();
            #endregion

            #endregion

        }
    }
}