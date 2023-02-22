using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Repository.Context;

namespace TesteHyperativa_Repository.Repositories
{
    public class CreditCardRepository : EfCoreRepository<CreditCard, HyperativaDBContext>, ICreditCardRepository
    {
        private readonly HyperativaDBContext _context;
        public CreditCardRepository(HyperativaDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
