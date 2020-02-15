using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using ExchangeRates;
using System.Xml.Linq;
using DB;
using System.Linq;

namespace ExchangeRatesUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeRatesReader reader = new ExchangeRatesReader();

            try
            {
                reader.Read();
                DB.ExchangeRates.Update(reader.Currencies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
