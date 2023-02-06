using _214247C_ICA1.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _214247C_ICA1.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        // kill the session and clean the session away; SignOutAsync
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Login");
        }
        // this guy will go back to the index page
        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}
