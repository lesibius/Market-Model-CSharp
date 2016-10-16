using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    /* *****************************************************************************************************************
     *                                          Building Block for Currencies                                           *
     * *****************************************************************************************************************/

    /// <summary>
    /// The <c>CurrencyClass</c> object is the basis to implement the <c>ValueClass</c> object and to perform operations on currency
    /// </summary>
    public class CurrencyClass
    {
        /// <summary>
        /// Constructor for the <c>CurrencyClass</c>
        /// </summary>
        /// <param name="uName">Plain name of the currency (e.g. Euro, British Pound) </param>
        /// <param name="uISO">Code ISO of the currency (e.g. EUR, GBP)</param>
        public CurrencyClass(string uName, string uISO)
        {
            Name = uName;
            CodeISO = uISO;
        }

        /// <summary>
        /// Name of the currency
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Code ISO of the currency
        /// </summary>
        public string CodeISO { get; set; }

        /// <summary>
        /// Override of the <c>GetHashCode</c> method of the <c>CurrencyClass</c> class
        /// </summary>
        /// <returns>The hash code of the <c>CurrencyClass</c>, based on the hash code of the <c>CodeISO</c> property </returns>
        public override int GetHashCode()
        {
            //The hash code of the CodeISO property is self-sufficient
            return CodeISO.GetHashCode();
        }

        /// <summary>
        /// Override the <c>Equals</c> method of the <c>CurrencyClass</c> class
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if both <c>CodeISO</c> are the same</returns>
        public override bool Equals(object obj)
        {
            var item = obj as CurrencyClass;
            if(item == null){ return false; }   //False if obj is null
            return this.CodeISO.Equals(item.CodeISO);
        }

    }

    /* ******************************************************************************************************************
     *                                                      Currency Pairs                                              *
     * *****************************************************************************************************************/

    /*  Currency pairs are used to create exchange rates
     *  Three classes are considered as currency pairs:
     *      1)  The <CurrencyPair> itself, which is the superclass from which the two other derive. It has only a limited use as it
     *              cannot contains the exchange rate itself
     *      2)  The <CurrencyPairSimpleRate> which is a subclass of <CurrencyPair> and can only contain one value to make the exchange.
     *              This forbid the use of bid-ask spread or mid-price 
     *      3)  The <CurrencyPairMarketRate> which is a subclass of <CurrencyPair> as well, but allows the user to provide a bid price
     *              and an ask price.
     */


    /// <summary>
    /// The <c>CurrencyPair</c> class is the basis to create <C>CurrencyExchange</C> instances
    /// It contains two currencies and some related method
    /// Remark: the order in which the <c>BaseCurrency</c> and <c>TargetCurrency</c> are provided are irrelevant when using the <c>Equals</c> method
    /// </summary>
    public class CurrencyPair
    {
        /// <summary>
        /// Constructor of the <c>CurrencyPair</c> class.
        /// The <c>CurrencyPair</c> in itself does not contain any information about the exchange rate. This information is contained in its inherited classes.
        /// </summary>
        /// <param name="CUR1">Currency that will be used as the base currency</param>
        /// <param name="CUR2">Currency that will be used as the target currency</param>
        public CurrencyPair(CurrencyClass CUR1, CurrencyClass CUR2)
        {
            //There is no importance over which one is the base currency in this case
            //This is handled by the CurrencyExchange class
            BaseCurrency = CUR1;
            TargetCurrency = CUR2;
        }
        /// <summary>
        /// Base currency of the pair.
        /// </summary>
        public CurrencyClass BaseCurrency { get; private set; }
        /// <summary>
        /// Target currency of the pair.
        /// </summary>
        public CurrencyClass TargetCurrency { get; private set; }

        //Overload hashcode here with this rule:
        // (CUR 1, CUR 2) == (CUR 2, CUR 1)
        public override int GetHashCode()
        {
            int minHashCode = Math.Min(BaseCurrency.GetHashCode(), TargetCurrency.GetHashCode());               //Start creating the hash code with the min value
            int maxHashCode = Math.Max(BaseCurrency.CodeISO.GetHashCode(), TargetCurrency.GetHashCode());       //Then use the max value
            int hash = 13;
            hash = (hash * 7) + minHashCode;
            hash = (hash * 7) + maxHashCode;
            return hash;    //The hashcode is the same whether CUR1 is the base currency or the target currency
        }
        /// <summary>
        /// Override the <c>Equals</c> method the <c>CurrencyPair</c> class
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if the pair is the same (no matter the order of the currency)</returns>
        public override bool Equals(object obj)
        {
            bool isSamePair;

            var item = obj as CurrencyPair;
            if(item == null) { return false; }

            isSamePair = this.BaseCurrency.Equals(item.BaseCurrency) && this.TargetCurrency.Equals(item.TargetCurrency);
            isSamePair = isSamePair || this.TargetCurrency.Equals(item.BaseCurrency) && this.BaseCurrency.Equals(item.TargetCurrency);

            return isSamePair;
        }

        public override string ToString()
        {
            return BaseCurrency.CodeISO + "/" + TargetCurrency.CodeISO;
        }

    }

    public class CurrencyPairSimpleRate:CurrencyPair
    {
        public CurrencyPairSimpleRate(
            CurrencyClass   CUR1,
            CurrencyClass   CUR2,
            decimal         uExchangeRate = 1
            ):base(CUR1,CUR2)
        {
            pExchangeRate = new ValueClass(CUR1, uExchangeRate);

        }
        private ValueClass pExchangeRate;

        public ValueClass ExchangeRate
        {
            get { return pExchangeRate; }
            set { pExchangeRate = value; }
        }

        public override string ToString()
        {
            return base.ToString() + " - " + ExchangeRate.Worth;
        }

    }

    public class CurrencyPairMarketRate:CurrencyPair
    {
        public CurrencyPairMarketRate(
            CurrencyClass   CUR1,
            CurrencyClass   CUR2,
            decimal         uBID,
            decimal         uASK
            ):base(CUR1, CUR2)
        {
            pBID = new ValueClass(CUR1,uBID);
            pASK = new ValueClass(CUR1, uASK);

        }
        private ValueClass pBID;
        public ValueClass BID
        {
            get { return pBID; }
            private set { pBID = value; }
        }

        private ValueClass pASK;
        public ValueClass ASK
        {
            get { return pASK; }
            set { pASK = value; }
        }

        public override string ToString()
        {
            return base.ToString() + " - " + BID.Worth + "/" + ASK.Worth;
        }
    }


    public class CurrencyExchange
    {
        public void AddPair(CurrencyPair uPair)
        {
            Pairs.Add(uPair);
        }

        HashSet<CurrencyPair> Pairs { get; }

    }

    public class SimpleCurrencyExchange:CurrencyExchange
    {
        public SimpleCurrencyExchange():base()
        {
            Pairs = new HashSet<CurrencyPairSimpleRate>();
            Currencies = new HashSet<CurrencyClass>();
        }

        public void AddPair(CurrencyPairSimpleRate uPair)
        {
            //TODO: check if the pair is already added
            //If yes: Exception or modify pair method?
            //Then, add all pairs involving the newly added currency(ies)
            Pairs.Add(uPair);
            Currencies.Add(uPair.BaseCurrency);
            Currencies.Add(uPair.TargetCurrency);
        }

        public HashSet<CurrencyPairSimpleRate> Pairs { get; }
        public HashSet<CurrencyClass> Currencies { get; set; }
    }
}
