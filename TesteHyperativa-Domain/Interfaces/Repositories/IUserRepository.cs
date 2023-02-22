using TesteHyperativa_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteHyperativa_Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> Get(string userName, string password);
    }
}
