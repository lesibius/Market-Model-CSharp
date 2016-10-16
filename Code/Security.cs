using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    /// <summary>
    /// The security class is the basis to implement any other security in the code.
    /// It only contains an ID (typically the ISIN) of the asset, it's name, and it's value, using a ValueClass object.
    /// </summary>
    public class SecurityClass
    {
        /// <summary>
        /// Constructor for the <c>Security</c> class.
        /// </summary>f
        /// <param name="uID">ID of the security (e.g. its ISIN)</param>
        /// <param name="uName">Name of the asset, in plain english</param>
        /// <param name="uCurrency">Currency objet for the computation basis of the security's value</param>
        /// <param name="uWorth">Initial value in the basis currency of the security. Default value of 0</param>
        public SecurityClass(string uID, string uName, CurrencyClass uCurrency, decimal uWorth = 0)
        {
            ID = uID;
            Name = uName;
            IntrinsicValue = new ValueClass(uCurrency, uWorth);
        }
        
        /// <summary>
        /// Intrinsic value of the <c>SecurityClass</c> instance
        /// </summary>
        public ValueClass IntrinsicValue { get; set; }
        /// <summary>
        /// ID (e.g. ISIN) of the <c>SecurityClass</c> instance
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Plain name of the <c>SecurityClass</c> instance
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Capital assets is the main branch of securities for bonds and equities
    /// </summary>
    public class CapitalAsset:SecurityClass
    {
        public CapitalAsset(string uID, string uName,  CurrencyClass uCurrency, decimal uWorth = 0):base(uID, uName, uCurrency,uWorth)
        {

        }
    }


    

    /// <summary>
    /// Main branch for commodity securities.
    /// </summary>
    public class Commodity:SecurityClass
    {
        public Commodity(string uID, string uName, CurrencyClass uCurrency, decimal uWorth = 0):base(uID, uName, uCurrency, uWorth)
        {

        }
    }


}
