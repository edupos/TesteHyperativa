
using TesteHyperativa_Domain.Entities;

namespace TesteHyperativa_Domain.Interfaces.Repositories
{
    public interface ICreditCardRepository : IRepository<CreditCard>
    {
        Task<CreditCard> GetByCard(string cardNumber);
    }
}
