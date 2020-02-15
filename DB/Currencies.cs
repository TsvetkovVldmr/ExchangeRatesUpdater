using System;
using System.Collections.Generic;

namespace DB
{
    public partial class Currencies
    {
        public Currencies()
        {
            ExchangeRates = new HashSet<ExchangeRates>();
        }

        public string Currency { get; set; }
        public string CurrencyCode { get; set; }

        public virtual ICollection<ExchangeRates> ExchangeRates { get; set; }
    }
}
