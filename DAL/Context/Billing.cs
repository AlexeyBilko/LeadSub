using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class Billing
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool ifPaid { get; set; }


        public int BilledPagesId { get; set; }
        public virtual BilledPages BilledPages { get; set; }
    }
}
