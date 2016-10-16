using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Model_CSharp
{
    /// <summary>
    /// Basic building block for classes requiring a yield
    /// </summary>
    public class Yield
    {
        /// <summary>
        /// Constructor of a yield object
        /// </summary>
        /// <param name="uStartDate">Starting date to compute the yield</param>
        /// <param name="uEndDate">Ending date to compute the yield</param>
        /// <param name="uValStart">Value at starting date</param>
        /// <param name="uValEnd">Value at ending date</param>
        public Yield(DateTime uStartDate, DateTime uEndDate, decimal uValStart, decimal uValEnd)
        {
            StartDate = uStartDate;
            EndDate = uEndDate;
            //Use decimal, ValueClass or both to generate yield???
            
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Factor { get; } 

        public static decimal HoldingPeriodYield(decimal valStart, decimal valEnd)
        {
            return (valEnd / valStart - 1);
        }

        public static decimal HoldingPeriodYield(ValueClass valStart, ValueClass valEnd)
        {
            return (valEnd.Worth / valStart.Worth - 1);
        }
    }

    public class InterestRate
    {
        public InterestRate(DateTime uDate, decimal uRate)
        {
            Date = uDate;
            Rate = uRate;
        }

        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
    }

    /// <summary>
    /// A <c>YieldCurve</c> object is constituted of <code>InterestRate</code> object to create the tenors.
    /// </summary>
    public class YieldCurve
    {
        public YieldCurve()
        {

        }

        public InterestRate[] Tenors { get; }
    }

    public class PlainVanillaIntrinsicValue
    {
        public void ComputeFromParRate(InterestRate uInterestRate, PlainVanillaBond uBond, DateTime uDate)
        {

        }
    }


    public class FixedIncomeInstrument : CapitalAsset
    {
        public FixedIncomeInstrument(string uID, string uName, CurrencyClass uCurrency, decimal uWorth = 0) : base(uID, uName, uCurrency, uWorth)
        {

        }
    }

    public class Bond : FixedIncomeInstrument
    {
        public Bond(string uID, string uName, CurrencyClass uCurrency, decimal uWorth = 0) : base(uID, uName, uCurrency, uWorth)
        {

        }
    }


    public class PlainVanillaBond : Bond
    {
        public PlainVanillaBond(
            string uID,
            string uName,
            CurrencyClass uCurrency,
            DateTime uIssueDate,
            DateTime uRedemptionDate,
            decimal uWorth = 0
            ) : base(uID, uName, uCurrency, uWorth)
        {
            IssueDate = uIssueDate;
            RedemptionDate = uRedemptionDate;
            DateTime[] tempSchedule = new DateTime[1];
            tempSchedule[0] = uRedemptionDate;
            CashFlowSchedule = tempSchedule;
        }



        public PlainVanillaBond(
            string uID,
            string uName,
            CurrencyClass uCurrency,
            DateTime uIssueDate,
            DateTime uRedemptionDate,
            int uNumberOfCashFlow,
            decimal uWorth = 0
            ) : this(uID, uName, uCurrency, uIssueDate, uRedemptionDate, uWorth)
        {
            //Assumes equally spaced cash flows
            double nDay = (uRedemptionDate - uIssueDate).TotalDays;
            int i;

            DateTime[] pTempSchedule = new DateTime[uNumberOfCashFlow];
            DateTime tempCashFlowDay = uIssueDate;

            for (i = 0; i <= uNumberOfCashFlow - 1; i++)
            {
                pTempSchedule[i] = uIssueDate.AddDays((i + 1) * nDay / uNumberOfCashFlow);
            }

            CashFlowSchedule = pTempSchedule;
        }


        //Manage issuance date
        public DateTime IssueDate { get; private set; }

        //Manage redemption date
        public DateTime RedemptionDate { get; private set; }

        //Cash flow schedule
        public DateTime[] CashFlowSchedule { get; private set; }

    }
}
