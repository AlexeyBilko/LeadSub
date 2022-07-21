using AutoMapper;
using BLL.DTO;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IdentityServices
{
    public class RoleService
    {
        RoleManager<IdentityRole> roleManager;
        IMapper mapper;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            MapperConfiguration configuration = new MapperConfiguration(opt =>
            {
                opt.CreateMap<IdentityRole, RoleDTO>();
                opt.CreateMap<RoleDTO, IdentityRole>();
            });
            mapper = new Mapper(configuration);
           // roleManager.
        }

    }
}
