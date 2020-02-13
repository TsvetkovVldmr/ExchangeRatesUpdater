using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRates
{
    public struct Currency
    {
        public string ID { get; private set; }
        public int NumCode { get; private set; }
        public string CharCode { get; private set; }
        public int Nominal { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

    }
}
