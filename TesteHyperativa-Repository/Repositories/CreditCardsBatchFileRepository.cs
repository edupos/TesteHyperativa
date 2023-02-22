using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Repository.Context;

namespace TesteHyperativa_Repository.Repositories
{
    public class CreditCardsBatchFileRepository : EfCoreRepository<CreditCardsBatchFile, HyperativaDBContext>, ICreditCardsBatchFileRepository
    {
        private readonly HyperativaDBContext _context;
        public CreditCardsBatchFileRepository(HyperativaDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
