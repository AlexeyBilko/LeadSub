using AutoMapper;
using BLL.DTO;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IdentityServices
{
    public class UserService
    {
        UserManager<User> userManager;
        IMapper mapper;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
            MapperConfiguration configuration = new MapperConfiguration(opt =>
            {
                opt.CreateMap<User,UserDTO>();
                opt.CreateMap<UserDTO,User>();
            });
            mapper = new Mapper(configuration);
        }
        public async Task<IdentityResult> CreateAsync(UserDTO user)
        {
            User newUser = mapper.Map<UserDTO, User>(user);
            var res=await userManager.CreateAsync(newUser);
            return res;
        }
        public async Task<UserDTO> GetUser(ClaimsPrincipal claims)
        {
            UserDTO user = mapper.Map<User,UserDTO>(await userManager.GetUserAsync(claims));
            return user;
        }
        public string GetUserId(ClaimsPrincipal claims)
        {
            return userManager.GetUserId(claims);
        }
        //public Task<UserDTO>FindByEmailAsync

    }
}
