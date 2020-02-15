using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ExchangeRates
{
    /// <summary>
    /// Курс валюты
    /// </summary>
    public sealed class Currency
    {
        /// <summary>
        /// Дата, на которую указан курс
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Буквенный код валюты
        /// </summary>
        public string CharCode { get; private set; }
        /// <summary>
        /// Номинал
        /// </summary>
        public int Nominal { get; private set; }
        /// <summary>
        /// Курс валюты
        /// </summary>
        public float Value { get; private set; }

        public Currency(DateTime date, string charCode, string nominal, string value)
        {
            Date = date;
            SetValue(charCode, nominal, value);
        }

        /// <summary>
        /// Установка свойств
        /// </summary>
        /// <param name="charCode">Буквенный код валюты</param>
        /// <param name="nominal">Номинал</param>
        /// <param name="value">Курс</param>
        private void SetValue(string charCode, string nominal, string value)
        {
            // Принимая вовнимание, что это сайт ЦБ и он стабилен,
            // предполагаю, что все параметры всегда корректны и не произвожу валидацию.
            CharCode = charCode;
            Nominal = Convert.ToInt32(nominal, CultureInfo.CurrentCulture);
            Value = Convert.ToSingle(value, CultureInfo.CurrentCulture);
        }

    }
}
