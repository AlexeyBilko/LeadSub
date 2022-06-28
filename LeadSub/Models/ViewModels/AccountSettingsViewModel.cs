using System.ComponentModel.DataAnnotations;

namespace LeadSub.Models.ViewModels
{
    public class AccountSettingsViewModel
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmationOldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get;set; }
        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        public int TotalFollowers { get; set; }
    }
}
