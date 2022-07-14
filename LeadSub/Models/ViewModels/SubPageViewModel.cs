using System.ComponentModel.DataAnnotations;

namespace LeadSub.Models.ViewModels
{
    public class SubPageViewModel
    {
        public int SubPageId { get; set; } = 0;
        [Required]
        public string? InstagramLink { get; set; }
        [Required]
        public string? MaterialLink { get; set; }
        [Required]
        public string? Title { get; set; }
        public IFormFile? Avatar { get; set; }
        [Required]
        public string? Header { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? GetButtonTitle { get; set; }
        public IFormFile? MainImage { get; set; }
        [Required]
        public string? SuccessDescription { get; set; }
        [Required]
        public string? SuccessButtonTitle { get; set; }


        public string? AvatarBase64 { get; set; }
        public string? MainImageBase64 { get; set; }
        public int SubscriptionsCount { get; set; } = 0;
        public int ViewsCount { get; set; } = 0;
        public DateTime CreationDate { get; set; } = DateTime.Now;


    }
}
