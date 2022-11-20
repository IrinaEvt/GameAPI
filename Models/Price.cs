using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Price
    {
        public Price(decimal amount, string currencyCode)
        {
            Amount = amount;
            Currency = currencyCode;

            if (amount < 0) throw new ArgumentException("Money could not be negative", nameof(amount));
            if (string.IsNullOrEmpty(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));
            if (CurrencyCode.IsValid(currencyCode) == false) throw new ArgumentException("Invalid Currency code", nameof(currencyCode));
        }

        public decimal Amount{ get; set; }
        public string Currency{ get; set; }

    }
}
