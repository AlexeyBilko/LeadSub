using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BillingRepository : GenericRepository<Billing>
    {
        public BillingRepository(DbContext context) : base(context)
        {
        }
    }
}
