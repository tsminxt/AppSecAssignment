using _214247C_ICA1.Model;
using _214247C_ICA1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _214247C_ICA1.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        // cannot see infront in html if nvr put bindproperty
        [BindProperty]
        public ResetPassword ResetModel { get; set; }

        public ResetPasswordModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return Page();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.ResetModel.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await userManager.ResetPasswordAsync(user, model.ResetModel.Token, model.ResetModel.Password);
                    if (result.Succeeded)
                    {
                        return Redirect("Account/ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return Page();
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return Redirect("Account/ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return Page();
        }
        
        public void OnGet()
        {
        }
    }
}
