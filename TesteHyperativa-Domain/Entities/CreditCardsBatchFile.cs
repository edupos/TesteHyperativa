using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteHyperativa_Domain.Interfaces.Entities;

namespace TesteHyperativa_Domain.Entities
{
    public class CreditCardsBatchFile : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public DateTime LoteDate { get; set; }
        public DateTime InputDate { get; set; }
        public string CreditCardsText { get; set; }
        public int AmountOfRecords { get; set; }
    }
}
