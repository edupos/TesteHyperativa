using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteHyperativa_Repository.Context;

namespace TesteHyperativa_Repository.Repositories
{
    public class UserRepository : EfCoreRepository<User, HyperativaDBContext>, IUserRepository
    {
        private readonly HyperativaDBContext _context;
        public UserRepository(HyperativaDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> Get(string userName, string password)
        {
            return _context.Set<User>().Where(o => o.UserName == userName && o.Password == password).SingleOrDefault();
        }
    }
}
