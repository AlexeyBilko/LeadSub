using LeadSub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadSub.Models
{
    public class AspNetIdentityContext:IdentityDbContext<User>
    {
      
       public AspNetIdentityContext(DbContextOptions<AspNetIdentityContext> options) :base(options)
       {
           Database.EnsureCreated();
       }
       
    }
}
