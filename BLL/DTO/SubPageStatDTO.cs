using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SubPageStatDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Subscriptions { get; set; }
        public int Views { get; set; }

        public int SubPageId { get; set; }
    }
}
