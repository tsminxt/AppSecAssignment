using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace _214247C_ICA1.ViewModels
{
    public class Register
    {
        // tell wat the model have; fulfill the database; rest is standard; identity have hashing @ required

        [Required]
        public string Fname { get; set; } = string.Empty;

        [Required]
        public string Lname { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[STFG]\d{7}[A-Z]$", ErrorMessage = "Invalid NRIC."), MaxLength(9)]
        public string NRIC { get; set; } = string.Empty;


        [Required, RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Please include @ in your email"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(12, ErrorMessage = "Passwords must be at least 12 characters long and contain at least an uppercase letter, lower case letter, digit and a symbol")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$", ErrorMessage = "Passwords must be at least 12 characters long and contain at least an uppercase letter, lower case letter, digit and a symbol")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MinLength(12, ErrorMessage = "Please rekey in the 12 characters password as above")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$", ErrorMessage = "Confirm Password is not the same")]
        public string CnfmPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Date is required")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Resume { get; set; }

        [Required]
        //allowing all special characters
        [RegularExpression(@"[><?@+'`~^%&\*\[\]\{\}.!#|\\\""$';,:;=/\(\),\-\w\s+]*", ErrorMessage = "Please add in a special character")]
        public string WhoamI { get; set; } = null!;
    }
}
