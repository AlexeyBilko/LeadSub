using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeadSub.Models.ViewModels
{
    public class RestorePasswordViewModel
    {
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        //public string ReturnUrl { get; set; }
    }
}
