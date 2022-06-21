namespace LeadSub.Models.ViewModels
{
    public class AccountSettingsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string OldPassword { get;set; }
        public string ConfirmationPassword { get; set; }
        public string NewPassword { get;set; }
        public int TotalFollowers { get; set; }
    }
}
