using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class LeadSubContext:IdentityDbContext<User>
    {
        public DbSet<SubPage> SubPages { get; set; }
        public DbSet<SubPageStat> SubPageStats { get; set; }
        public DbSet<BilledPages> BilledPages { get; set; }
        public DbSet<Billing> Billings { get; set; }

        public DbSet<Link> Links { get; set; }
        public DbSet<LinkPage> LinkPages { get; set; }

        public LeadSubContext(DbContextOptions<LeadSubContext>options):
            base(options)
        {
            Database.EnsureCreated();
        }
    }
}
