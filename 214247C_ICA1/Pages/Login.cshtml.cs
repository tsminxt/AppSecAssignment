using _214247C_ICA1.Model;
using _214247C_ICA1.ViewModels;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace _214247C_ICA1.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private UserManager<ApplicationUser> userManager { get; }

        private readonly SignInManager<ApplicationUser> signInManager;
        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        // check whether is the user a not.
        public void OnGet()
        {
        }
		[ValidateReCaptcha]
		public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                LModel.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    //Create the security context
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, "c@c.com"),
                        new Claim(ClaimTypes.Email, "c@c.com"),
                        new Claim("Membership", "HR")
                    };
                    var i = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(login.Email);
                if (User != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(User, login.Password, false, true);
                    if (result.Succeeded)
                        return RedirectToPage("/Index");

                    if (result.IsLockedOut)
                        ModelState.AddModelError("", "Your account is locked out. Kindly wait for 2 minutes and try again");
                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return RedirectToPage("/Login");
        }
    }
}