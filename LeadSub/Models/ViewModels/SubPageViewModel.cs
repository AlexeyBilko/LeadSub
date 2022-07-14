using System.ComponentModel.DataAnnotations;

namespace LeadSub.Models.ViewModels
{
    public class SubPageViewModel
    { 
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
    }
}
