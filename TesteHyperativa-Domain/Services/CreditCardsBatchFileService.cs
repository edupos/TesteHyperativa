using Microsoft.AspNetCore.Http;
using TesteHyperativa_Domain.Entities;
using TesteHyperativa_Domain.Interfaces.Repositories;
using TesteHyperativa_Domain.Interfaces.Services;

namespace TesteHyperativa_Domain.Services
{
    public class CreditCardsBatchFileService : Service<CreditCardsBatchFile>, ICreditCardsBatchFileService
    {
        private readonly ICreditCardsBatchFileRepository _repository;
        private readonly ICreditCardService _creditCardService;
        public CreditCardsBatchFileService(ICreditCardsBatchFileRepository repository, ICreditCardService creditCardService) : base(repository)
        {
            _repository = repository;
            _creditCardService = creditCardService;
        }

        public async Task<CreditCardsBatchFile> ProcessFileHeaderData(IFormFile form)
        {
            var fileText = await ReadFormFileAsync(form);
            var headLine = fileText.Split('\n')[0];
            var schema = await FillHeadSchema();

            CreditCardsBatchFile result = new CreditCardsBatchFile();
            var NomeField = schema.Where(o => o.Data == "NOME").FirstOrDefault();
            result.Nome = headLine.Substring(NomeField.StartPosition - 1, NomeField.EndPosition - NomeField.StartPosition + 1).Trim();
            
            var DataField = schema.Where(o => o.Data == "DATA").FirstOrDefault();
            
            String dataString = headLine.Substring(DataField.StartPosition - 1, DataField.EndPosition - DataField.StartPosition + 1).Trim();
            dataString = dataString.Insert(6, "/");
            dataString = dataString.Insert(4, "/");
            result.LoteDate = DateTime.Parse(dataString);

            var LoteField = schema.Where(o => o.Data == "LOTE").FirstOrDefault();
            result.Lote = headLine.Substring(LoteField.StartPosition - 1, LoteField.EndPosition - LoteField.StartPosition + 1).Trim();
            
            var QtdRegistrosField = schema.Where(o => o.Data == "QTD DE REGISTROS").FirstOrDefault();
            result.AmountOfRecords = int.Parse(headLine.Substring(QtdRegistrosField.StartPosition - 1, QtdRegistrosField.EndPosition - QtdRegistrosField.StartPosition + 1).Trim());
            
            result.CreditCardsText = CryptographyService.Codificar(fileText);
            result.InputDate = DateTime.Now;

            await Add(result);

            return result;
        }

        public async Task<List<CreditCard>> ProcessFileBodyData(IFormFile form, int AmountOfRecords, string userId)
        {
            var fileText = await ReadFormFileAsync(form);
            var stringLines = fileText.Split('\n');
            var schema = await FillBodySchema();

            List<CreditCard> result = new List<CreditCard>();

            for (int line = 1; line <= AmountOfRecords; line++)
            {

                CreditCard cardLine = new CreditCard();
                var NumberField = schema.Where(o => o.Data == "NÚMERO DE CARTAO COMPLETO").FirstOrDefault();
                cardLine.Number = CryptographyService.Codificar(stringLines[line].Substring(NumberField.StartPosition - 1, (stringLines[line].Length < NumberField.EndPosition? stringLines[line].Length : NumberField.EndPosition) - NumberField.StartPosition + 1).Trim());

                cardLine.InputUserId = userId;
                cardLine.InputDate = DateTime.Now;
                cardLine.InputMode = Enum.EnumInputMode.BatchFile;

                await _creditCardService.Add(cardLine);
                result.Add(cardLine);
            }

            return result;
        }

        public static async Task<string> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return await Task.FromResult((string)null);
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<List<FileSchema>> FillHeadSchema()
        {
            List<FileSchema> result = new List<FileSchema>();
            result.Add(new FileSchema() { Data = "NOME", StartPosition = 1, EndPosition = 29 });
            result.Add(new FileSchema() { Data = "DATA", StartPosition = 30, EndPosition = 37 });
            result.Add(new FileSchema() { Data = "LOTE", StartPosition = 38, EndPosition = 45 });
            result.Add(new FileSchema() { Data = "QTD DE REGISTROS", StartPosition = 46, EndPosition = 51 });
            return result;
        }

        public async Task<List<FileSchema>> FillBodySchema()
        {
            List<FileSchema> result = new List<FileSchema>();
            result.Add(new FileSchema() { Data = "IDENTIFICADOR DA LINHA", StartPosition = 1, EndPosition = 1 });
            result.Add(new FileSchema() { Data = "NUMERAÇÃO NO LOTE", StartPosition = 2, EndPosition = 7 });
            result.Add(new FileSchema() { Data = "NÚMERO DE CARTAO COMPLETO", StartPosition = 08, EndPosition = 26 });
            return result;
        }

    }
}
