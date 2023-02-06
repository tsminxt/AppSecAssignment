using _214247C_ICA1.Model;
using _214247C_ICA1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace _214247C_ICA1.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        //getting from app identityyy
        public ApplicationUser UserIden { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private IDataProtectionProvider _dataProtectionProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager { get; }

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDataProtectionProvider dataProtectionProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }
        public async Task OnGet()
        {
            UserIden = await _userManager.GetUserAsync(User);
            var protector = _dataProtectionProvider.CreateProtector("MySecretKey");
            var user = await _userManager.GetUserAsync(User);
            user.NRIC = protector.Unprotect(user.NRIC);
            //System.Diagnostics.Debug.WriteLine(decrypted);
        }
    }
}