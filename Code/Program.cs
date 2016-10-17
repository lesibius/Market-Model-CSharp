using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Let's define some currencies
            CurrencyClass EUR = new CurrencyClass("Euro",               "EUR");
            CurrencyClass USD = new CurrencyClass("US Dollar",          "USD");
            CurrencyClass CHF = new CurrencyClass("Swiss France",       "CHF");
            CurrencyClass JPY = new CurrencyClass("Japan Yen",          "JPY");

            //Let's create some pairs of currencies
            CurrencyPairSimpleRate EURUSD = new CurrencyPairSimpleRate(EUR, USD, 1.10m);
            CurrencyPairSimpleRate USDEUR = new CurrencyPairSimpleRate(USD, EUR, 1 / 1.10m);        //This one equals the previous
            CurrencyPairMarketRate EURUSDM = new CurrencyPairMarketRate(EUR, USD, 1.09m, 1.11m);
            CurrencyPairSimpleRate EURCHF = new CurrencyPairSimpleRate(EUR, CHF, 1.09m);
            CurrencyPairMarketRate EURCHFM = new CurrencyPairMarketRate(EUR, CHF, 1.088m, 1.092m);
            CurrencyPairSimpleRate EURJPY = new CurrencyPairSimpleRate(EUR, JPY, 114.28m);

            //Let's create two exchange currency
            //NB: this is not an <Organization>, but it will probably added to one later
            SimpleCurrencyExchange SimpleForex = new SimpleCurrencyExchange();


            //Let's add the predfined pairs
            SimpleForex.AddPair(EURUSD);
            SimpleForex.AddPair(USDEUR); //This will do nothing
            SimpleForex.AddPair(EURCHF);
            SimpleForex.AddPair(EURJPY);
            
            //Get all pairs and print it
            Dictionary<CurrencyPair,CurrencyPairSimpleRate> allPairs = SimpleForex.Pairs;
            foreach(CurrencyPairSimpleRate pair in allPairs.Values)
            {
                Console.WriteLine("{0}", pair);
                //Why static does not work?
                Console.WriteLine("{0}", CurrencyPairSimpleRate.InversePair(pair));
            }

            DateTime issuance = new DateTime(1990, 4, 26);
            DateTime redemption = issuance.AddYears(30);

            PlainVanillaBond someBond = new PlainVanillaBond("FR666666", "Some French Bond" , EUR, issuance, redemption,5, 1000);
            Console.WriteLine("ISIN: {0} - value: {1}{2}", someBond.ID, someBond.IntrinsicValue.Currency.CodeISO, someBond.IntrinsicValue.Worth);
            Console.WriteLine("someBond value in JPY = {0}", SimpleForex.Convert(someBond.IntrinsicValue, JPY));
            //Let's change the value of the bond:
            PlainVanillaBond anotherBond = someBond;
            someBond.IntrinsicValue += 12.24m;
            
            Console.WriteLine("ISIN: {0} - value: {1}{2}", someBond.ID, someBond.IntrinsicValue.Currency.CodeISO, someBond.IntrinsicValue.Worth);
            Console.Write("ISIN: {0} - Issued: {1} - Redemption: {2}", someBond.ID, someBond.IssueDate,someBond.RedemptionDate);
            Console.WriteLine("Just to make sure what append with the '+' overload: the value of anotherBond: {0}", anotherBond.IntrinsicValue);
            int i;
            for (i = 0; i < someBond.CashFlowSchedule.Length; i++)
            { Console.WriteLine("Cash flow {1}: {0}", someBond.CashFlowSchedule[i], i+1); }


            Position pos = new Position(someBond, 12);

            
            
            

            SimpleCurrencyExchange simpleExchange = new SimpleCurrencyExchange();
            simpleExchange.AddPair(EURUSD);

            Console.WriteLine("{0} - {1}", EUR.GetHashCode(), USD.GetHashCode());
            Console.WriteLine("EUR = USD? {0}", EUR.Equals(USD));
            Console.WriteLine("EUR = EUR? {0}", EUR.Equals(EUR));
            Console.WriteLine("EURUSD is USDEUR? {0}", EURUSD.Equals(USDEUR));
            //Doesn't work because now a dictionary
            //Console.WriteLine("{0}",simpleExchange.Pairs.Contains(EURUSD));
            //Console.WriteLine("{0}", simpleExchange.Pairs.Contains(USDEUR));

            Console.WriteLine("{0}",EURUSD);
            Console.WriteLine("{0}", EURUSDM);

            //Adding two values
            ValueClass v1 = new ValueClass(EUR, 12.05m);
            ValueClass v2 = new ValueClass(EUR, 11.05m);
            ValueClass v3 = new ValueClass(USD, 11.05m);
            Console.WriteLine("v1 + v2 = {0}", v1 + v2);
            Console.WriteLine("12.05m + v1 = {0}",12.05m + v1);
            //Console.WriteLine("v1 + v3 = {0}", v1 + v3); //Throw an exception v1: v1 in EUR and v3 in USD

        }
    }
}
