using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class SubPageStat
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Subscriptions { get; set; }
        public int Views { get; set; }

        public int SubPageId;
        public virtual SubPage SubPage { get; set; }
    }
}
