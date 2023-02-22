using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Domain.Interfaces.Services;


namespace TesteHyperativa_Domain.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<User?> Get(string userName, string password) 
        {
            return _repository.Get(userName, password); 
        }
    }
}
