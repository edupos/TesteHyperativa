using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteHyperativa_Domain.Enum;
using TesteHyperativa_Domain.Interfaces.Entities;

namespace TesteHyperativa_Domain.Entities
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public EnumInputMode InputMode { get; set; }
        public DateTime InputDate { get; set; }
        public string InputUserId { get; set; }
    }
}
