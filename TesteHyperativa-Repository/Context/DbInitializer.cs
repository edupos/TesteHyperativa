using TesteHyperativa_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteHyperativa_Repository.Context
{
    public class DbInitializer
    {
        public static void Initialize(HyperativaDBContext context)
        {
            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            var users = new User[]
            {
                new User { Name = "Eduardo", UserName = "edu", Password = "edu", UserRole = "manager"},
                new User { Name = "Paulo", UserName = "paulo", Password = "paulo", UserRole = "employee"},
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

        }
    }
}
