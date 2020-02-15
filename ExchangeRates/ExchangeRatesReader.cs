using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace ExchangeRates
{
    /// <summary>
    /// Получает курс валют с сайта ЦБ
    /// </summary>
    public sealed class ExchangeRatesReader
    {
        // Часть пути, без запрашиваемой даты
        private const string uri = @"https://www.cbr.ru/scripts/XML_daily.asp?date_req=";
        private DateTime date;

        public List<Currency> Currencies { get; private set; }

        public ExchangeRatesReader()
        {
            date = DateTime.Now;
            Currencies = new List<Currency>();
        }

        /// <summary>
        /// Читает xml и заполняет коллекцию курсов валют.
        /// </summary>
        public void Read()
        {
            try
            {
                var document = GetDocument();
                SetCurrencies(document);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Получает xdoc с курсами валют на текущю дату
        /// </summary>
        /// <returns></returns>
        private XDocument GetDocument()
        {
            // Сайт ЦБ отдает xml в кодировке windows-1251
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var currentDate = date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            var fullUri = uri + currentDate;

            try
            {
                var document = XDocument.Load(fullUri);

                return document;
            }
            // Что-то не так с подключением
            catch (System.Net.WebException)
            {
                throw;
            }
        }

        /// <summary>
        /// Заполняет список валют
        /// </summary>
        /// <param name="document">XML document</param>
        private void SetCurrencies(XDocument document)
        {
            // Учитывая, что xml получен с сайта ЦБ, предполагаю, что его разметка не меняется.
            foreach (XElement node in document.Element("ValCurs").Elements("Valute"))
            {
                var currency = GetCurrency(node);
                Currencies.Add(currency);
            }
        }

        /// <summary>
        /// Создает объект курса валют из элемента xml
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Currency GetCurrency(XElement node)
        {
            XElement CharCode = node.Element("CharCode");
            XElement Nominal = node.Element("Nominal");
            XElement Value = node.Element("Value");


            return new Currency(date, CharCode.Value, Nominal.Value, Value.Value);
        }
    }
}
