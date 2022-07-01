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
    public class LinkService : GenericService<Link, LinkDTO>
    {
        public LinkService(IRepository<Link> repository) : base(repository)
        {
        }
    }
}
