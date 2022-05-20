using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubPageRepository : GenericRepository<SubPage>
    {
        public SubPageRepository(DbContext context) : base(context)
        {
        }
    }
}
