using System;
using System.Collections.Generic;
using ExchangeRates;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    /// <summary>
    /// Тип, соответствующий таблице Курс
    /// </summary>
    public partial class ExchangeRates
    {
        public DateTime RatesDate { get; set; }
        public string Currency { get; set; }
        public float Rate { get; set; }

        public virtual Currencies CurrencyNavigation { get; set; }

        /// <summary>
        /// Создает коллекцию объектов Курс базы данных без учета номинала валюты
        /// </summary>
        /// <param name="currencies"></param>
        /// <returns></returns>
        private static IEnumerable<ExchangeRates> GetExchangeRates(List<Currency> currencies)
        { 
            return currencies.Select(p => new ExchangeRates()
            {
                RatesDate = p.Date,
                Currency = p.CharCode,
                Rate = p.Value
            });
        }

        /// <summary>
        /// Создает коллекцию объектов Курс базы данных с учетом номинала валюты, 
        /// т.е. количество рублей за одну ед. валюты.
        /// </summary>
        /// <param name="currencies"></param>
        /// <returns></returns>
        private static IEnumerable<ExchangeRates> GetExchangeRatesWithNominals(List<Currency> currencies)
        {
            return currencies.Select(p => new ExchangeRates()
            {
                // Номинал равный нулю противоречит логике и не встречается,
                // поэтому проверка на ноль не реализована
                RatesDate = p.Date,
                Currency = p.CharCode,
                Rate = p.Value / p.Nominal
            });
        }

        /// <summary>
        /// Добавляет в бд коллеккцию курсов валют
        /// </summary>
        /// <param name="currencies"></param>
        public static void Update(List<Currency> currencies)
        {
            try
            {
                using (Context context = new Context())
                {
                    if (!context.Database.CanConnect())
                        throw new Exception("База данных недоступна");

                    var rates = GetExchangeRates(currencies);
                    context.AddRange(rates);
                    context.SaveChanges();
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw;
            }
        }
    }
}
