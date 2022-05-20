using BLL.DTO;
using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BilledPagesService : GenericService<BilledPages, BilledPagesDTO>
    {
        public BilledPagesService(IRepository<BilledPages> repository) : base(repository)
        {
        }
    }
}
