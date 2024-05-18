using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMRIFA.Model.Models.PayU
{
    public class OrderPayUDto
    {
        public string ApiKey { get; set; }
        public string ApiLogin { get; set; }
        public string AccountId { get; set; }
        public string ReferenceCode { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string NotifyUrl { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentCountry { get; set; }
        public string PayerFullName { get; set; }
        public string PayerEmailAddress { get; set; }
        public string PayerContactPhone { get; set; }
        public string BankCode { get; set; } // Solo para PSE
        public string CreditCardNumber { get; set; } // Solo para tarjeta de crédito
        public string CreditCardSecurityCode { get; set; } // Solo para tarjeta de crédito
        public string CreditCardExpirationDate { get; set; } // Solo para tarjeta de crédito
        public string CreditCardHolderName { get; set; } // Solo para tarjeta de crédito
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
