using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models.PayU
{
    public class ProcesarPagoPayUDto
    {
        public string AccessToken { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationDate { get; set; }
        public string CardHolderName { get; set; }
    }
}
