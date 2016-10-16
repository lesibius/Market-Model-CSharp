using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{

    public class GeographicalArea
    {
        public GeographicalArea(string uName)
        {
            Name = uName;
        }

        public string Name { get; set; }
    }

    public class Country: GeographicalArea
    {
        public Country(string uName) : base(uName)
        {

        }

        public decimal GDP { get; set; }
    }
}
