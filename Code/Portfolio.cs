using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    /// <summary>
    /// The <c>Position</c> class is the basis to create <c>Portoflio</c> instances
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Constructor of the <c>Position</c> class
        /// </summary>
        /// <param name="uSecurity">A <c>SecurityClass</c> instance that constitues the basis of the positions</param>
        /// <param name="uQuantity">Quantity of the <c>SecurityClass</c> instance in the position</param>
        public Position(SecurityClass uSecurity, decimal uQuantity)
        {
            Security = uSecurity;
            Quantity = uQuantity;

        }
        /// <summary>
        /// Security associated to the position
        /// </summary>
        public SecurityClass Security { get; private set; }
        /// <summary>
        /// Quantity associated to the position
        /// </summary>
        public decimal Quantity { get; private set; }
    }

    /// <summary>
    /// Basic <c>Portfolio</c> class
    /// </summary>
    public class Portfolio
    {
        public Portfolio(Position uPosition)
        {

        }

        public void Transaction()
        {

        }
        //TODO: add a hashset<Position> to carry position
        //TODO: add a Transaction method to buy/sell positions
        //Idea for transaction: Transaction(SecurityLong, SecurityShort, QuantityLong, QUantityShort): allows for transaction with something else but money
        //Use event handler and delegates: for instance, suscribe to bond/future/... and call transactions at expiry
    }
}
