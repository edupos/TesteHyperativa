using Microsoft.EntityFrameworkCore;
using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Domain.Services;
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

        public async Task<CreditCard?> GetByCard(string cardNumber)
        {
            return _context.Set<CreditCard>().Where(o => o.Number == CryptographyService.Codificar(cardNumber)).FirstOrDefault();
        }
    }
}
