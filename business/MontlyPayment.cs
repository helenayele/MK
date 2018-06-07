using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QisiqesanaGebiAsebaseb.business
{
    class MontlyPayment
    {
        public int DonerId
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public Dictionary<int, decimal> Monthly_payment_Dic
        {
            get;
            set;
        }
       
        public decimal PaymentAmount
        {
            get;
            set;
        }

        public DateTime PaymentDate
        {
            get;
            set;
        }
        public DateTime PaymentPeriod
        {
            get;
            set;
        }


        public string ProjectName
        {
            get;
            set;
        }

        public decimal PromisedAmount
        {
            get;
            set;
        }

        public string DonatingInterval
        {
            get;
            set;
        }

        public decimal TotalPrmisedAmount
        {
            get;
            set;
        }

    
    }
}
