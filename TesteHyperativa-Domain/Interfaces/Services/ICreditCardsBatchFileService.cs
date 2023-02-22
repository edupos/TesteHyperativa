
using Microsoft.AspNetCore.Http;
using TesteHyperativa_Domain.Entities;

namespace TesteHyperativa_Domain.Interfaces.Services
{
    public interface ICreditCardsBatchFileService : IService<CreditCardsBatchFile>
    {
        Task<CreditCardsBatchFile> ProcessFileHeaderData(IFormFile form);
        Task<List<CreditCard>> ProcessFileBodyData(IFormFile form, int AmountOfRecords, string userId);
    }
}
