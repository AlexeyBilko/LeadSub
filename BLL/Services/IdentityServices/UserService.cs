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
        public async Task<IdentityResult> CreateAsync(UserDTO user,string password)
        {
            user.Id = Guid.NewGuid().ToString();
            User newUser = mapper.Map<UserDTO, User>(user);
            newUser.UserName = user.Email;
            var res=await userManager.CreateAsync(newUser,password);
            return res;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal claims)
        {
            UserDTO user = mapper.Map<User,UserDTO>(await userManager.GetUserAsync(claims));
            return user;
        }
        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            UserDTO user = mapper.Map<User, UserDTO>(await userManager.FindByEmailAsync(email));
            return user;
        }
        public string GetUserId(ClaimsPrincipal claims)
        {
            return userManager.GetUserId(claims);
        }
        
        public async Task<IdentityResult> ChangePasswordAsync(string Email,string newPassword,string oldPassword)
        {
            User user=await userManager.FindByEmailAsync(Email);
            var res = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return res;
        }
        public async Task<IdentityResult>RestorePassword(string email,string newPassword)
        {
            User user = await userManager.FindByEmailAsync(email);
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            PasswordVerificationResult res = hasher.VerifyHashedPassword(user, user.PasswordHash, newPassword);
            if (res==PasswordVerificationResult.Failed)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                var identityRes = await userManager.ResetPasswordAsync(user, resetToken, newPassword);
                return identityRes;
            }
            IdentityError error = new IdentityError();
            error.Description = "Новий пароль не має співпадати зі старим!";
            return IdentityResult.Failed(error);
        }

    }
}
