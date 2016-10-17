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


        public delegate ValueClass DefaultValueConversion(ValueClass ValueToConvert);

        /// <summary>
        /// Override the <c>+</c> operator for <c>ValueClass</c> and <c>decimal</c>
        /// This assume that the decimal is expressed in term of currency of the <c>ValueClass</c> instance
        /// </summary>
        /// <param name="v1">Value in the + operation</param>
        /// <param name="val">Value (expressed in term of currency of v1) to add</param>
        /// <returns></returns>
        public static ValueClass operator +(ValueClass v1, decimal val)
        {
            return new ValueClass(v1.Currency, v1.Worth + val);
        }
        /// <summary>
        /// Override the <c>+</c> operator for <c>ValueClass</c> and <c>decimal</c>
        /// This assume that the decimal is expressed in term of currency of the <c>ValueClass</c> instance
        /// </summary>
        /// <param name="val">Value (expressed in term of currency of v1) to add</param>
        /// <param name="v1">Value in the + operation</param>
        /// <returns></returns>
        public static ValueClass operator +(decimal val, ValueClass v1) { return v1 + val; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static ValueClass operator +(ValueClass v1, ValueClass v2)
        {
            //If both currencies are the same, just add worth, else throw an exception
            if(v1.Currency.Equals(v2.Currency)){ return new ValueClass(v1.Currency, v1.Worth + v2.Worth);}
            else{throw new System.ArgumentException("Cannot add two ValueClass with different currencies","v1 and v2");}
        }


        public override string ToString()
        {
            return(this.Currency.CodeISO.ToString()+ " " + this.Worth.ToString());
        }

    }
    
}
