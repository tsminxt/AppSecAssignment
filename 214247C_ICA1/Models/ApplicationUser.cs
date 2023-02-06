using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _214247C_ICA1.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string Fname { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        public string Lname { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        public string NRIC { get; set; }

        [Required]
        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [PersonalData]
        public string ResumeURL { get; set; }

        [PersonalData]
        public string? WhoamI { get; set; }
    }
}
