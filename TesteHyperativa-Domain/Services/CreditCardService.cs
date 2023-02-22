using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Domain.Interfaces.Services;

namespace TesteHyperativa_Domain.Services
{
    public class CreditCardService : Service<CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _repository;
        public CreditCardService(ICreditCardRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<CreditCard> GetByCard(string cardNumber)
        {
            return _repository.GetByCard(cardNumber);
        }
    }
}
