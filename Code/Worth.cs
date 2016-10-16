using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    /// <summary>
    /// A <c>ValueClass</c> allows to attribute a value to a <c>SecurityClass</c> instance.
    /// It is made of a <c>CurrencyClass</c> instance and <c>decimal</c>.
    /// The <c>CurrencyClass</c> instance allows to determine the currency in which the value is expressed, while the value itself is retrieved using <c>Worth</c> property
    /// </summary>
    public class ValueClass
    {
        /// <summary>
        /// Constructor of the <c>ValueClass</c>
        /// It takes two compulsory parameters
        /// </summary>
        /// <param name="uCurrency"></param>
        /// <param name="uWorth"></param>
        public ValueClass(CurrencyClass uCurrency, decimal uWorth)
        {
            Currency = uCurrency;
            Worth = uWorth;
        }

        /// <summary>
        /// Currency in which the value is expressed
        /// </summary>
        public CurrencyClass Currency
        { get; private set; }

        /// <summary>
        /// Actual value expressed in amount of the associated currency
        /// </summary>
        public decimal Worth { get; set; }

        /// <summary>
        /// Convert a <c>ValueClass</c> instance to a new <c>ValueClass</c> instance in the specified currency, using a <c>CurrencyExchange</c> instance
        /// </summary>
        /// <param name="forex"><c>CurrencyExchange</c> instance which provide the exchange rate</param>
        /// <param name="val"><c>ValueClass</c> instance to convert</param>
        /// <returns>The original <c>ValueClass</c> instance converted in the new currency</returns>
        public static ValueClass Convert(CurrencyExchange forex, ValueClass val)
        {
            //TODO: call the forex.Convert method to return a new val
            //Use a delegate???
            return (val);
        }

        /*
        public static ValueClass operator +(ValueClass v1, ValueClass v2)
        {
            //Convert v2 currency

            //Add v1 and v2 Worth
        }
        */

    }
}
