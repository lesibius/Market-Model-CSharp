using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    public class Organization
    {
        public Organization(string uName)
        {
            Name = uName;
        }

        public string Name { get; set; }
    }


    public class CentralBank:Organization
    {
        public CentralBank(string uName, CurrencyClass uCurrency) : base(uName)
        {
            Currency = uCurrency;
        }

        public CurrencyClass Currency { get; set; }
    }
}
