using System.ComponentModel.DataAnnotations;

namespace LeadSub.Models.ViewModels
{
    public class SubscriptionCheckerViewModel
    {
        [Required]
        public string? Username { get; set; }
    }
}
