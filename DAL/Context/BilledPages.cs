using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class BilledPages
    {
        public int Id { get; set; }
        public int SubPageId { get; set; }
        public virtual ICollection<SubPage> SubPages{ get; set; }
    }
}
