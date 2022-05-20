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
    public class SubPagesStatService : GenericService<SubPageStat, SubPageDTO>
    {
        public SubPagesStatService(IRepository<SubPageStat> repository) : base(repository)
        {
        }
    }
}
