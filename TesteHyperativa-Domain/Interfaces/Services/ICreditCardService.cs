
using TesteHyperativa_Domain.Entities;

namespace TesteHyperativa_Domain.Interfaces.Services
{
    public interface ICreditCardService : IService<CreditCard>
    {
        Task<CreditCard> GetByCard(string cardNumber);
    }
}
