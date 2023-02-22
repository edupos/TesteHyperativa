using AutoMapper;
using TesteHyperativa_Domain.Interfaces.Services;
using TesteHyperativa_MinimalAPI.Services;
using TesteHyperativa_MinimalAPI.ViewModels;

namespace TesteHyperativa_MinimalAPI.Login
{
    public class Login
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public Login(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public IResult GetToken(LoginViewModel model)
        {
            var user = _userService.Get(model.UserName, model.Password).Result;

            if (user == null)
                return Results.NotFound(new { message = "Invalid user name or password." });

            var _mappedUserVM = _mapper.Map<UserViewModel>(user);

            var token = TokenService.GenerateToken(_mappedUserVM);

            user.Password = "";

            return Results.Ok(new
            {
                user = user,
                token = token
            });
        }
    }
}
