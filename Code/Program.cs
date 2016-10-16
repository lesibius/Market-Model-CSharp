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
            Country France = new Country("France");
            France.GDP = 1000.0m;

            Console.WriteLine("Country: {0}. GDP: {1}", France.Name, France.GDP);

            CurrencyClass EUR = new CurrencyClass("Euro", "EUR");
            CentralBank ECB = new CentralBank("ECB",EUR);

            Console.WriteLine("Central bank: {0} - Currency: {1} - ISO: {2}", ECB.Name, ECB.Currency.Name, ECB.Currency.CodeISO);

            DateTime issuance = new DateTime(1990, 4, 26);
            DateTime redemption = issuance.AddYears(30);

            PlainVanillaBond someBond = new PlainVanillaBond("FR666666", "Some French Bond" , EUR, issuance, redemption,5, 1000);
            Console.WriteLine("ISIN: {0} - value: {1}{2}", someBond.ID, someBond.IntrinsicValue.Currency.CodeISO, someBond.IntrinsicValue.Worth);

            //Let's change the value of the bond:
            someBond.IntrinsicValue.Worth += 12.24m;
            Console.WriteLine("ISIN: {0} - value: {1}{2}", someBond.ID, someBond.IntrinsicValue.Currency.CodeISO, someBond.IntrinsicValue.Worth);
            Console.Write("ISIN: {0} - Issued: {1} - Redemption: {2}", someBond.ID, someBond.IssueDate,someBond.RedemptionDate);
            
            int i;
            for (i = 0; i < someBond.CashFlowSchedule.Length; i++)
            { Console.WriteLine("Cash flow {1}: {0}", someBond.CashFlowSchedule[i], i+1); }


            Position pos = new Position(someBond, 12);

            CurrencyClass USD = new CurrencyClass("US Dollar", "USD");
            CurrencyPairSimpleRate EURUSD = new CurrencyPairSimpleRate(EUR, USD, 1.10m);
            CurrencyPairSimpleRate USDEUR = new CurrencyPairSimpleRate(USD, EUR, 1 / 1.10m);
            CurrencyPairMarketRate EURUSDM = new CurrencyPairMarketRate(EUR, USD, 1.09m, 1.11m);
            

            SimpleCurrencyExchange simpleExchange = new SimpleCurrencyExchange();
            simpleExchange.AddPair(EURUSD);

            Console.WriteLine("{0} - {1}", EUR.GetHashCode(), USD.GetHashCode());
            Console.WriteLine("EUR = USD? {0}", EUR.Equals(USD));
            Console.WriteLine("EUR = EUR? {0}", EUR.Equals(EUR));
            Console.WriteLine("EURUSD is USDEUR? {0}", EURUSD.Equals(USDEUR));

            Console.WriteLine("{0}",simpleExchange.Pairs.Contains(EURUSD));
            Console.WriteLine("{0}", simpleExchange.Pairs.Contains(USDEUR));

            Console.WriteLine("{0}",EURUSD);
            Console.WriteLine("{0}", EURUSDM);

        }
    }
}
