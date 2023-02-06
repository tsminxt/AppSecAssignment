using _214247C_ICA1.Model;
using _214247C_ICA1.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace _214247C_ICA1.Pages.Shared
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger logger;
        // two managers; user - create user in asp.net users, sign in is able to do a login
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        private readonly RoleManager<IdentityRole> roleManager;

        private IWebHostEnvironment _environment;

        private IDataProtectionProvider _dataProtectionProvider;

        // cannot see infront in html if nvr put bindproperty
        [BindProperty]
        public Register RModel { get; set; }
        public ApplicationUser UserIden { get; set; }
        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment, RoleManager<IdentityRole> roleManager, IDataProtectionProvider dataProtectionProvider, ILogger<RegisterModel> logger1)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._environment = environment;
            this._dataProtectionProvider = dataProtectionProvider;
            this.logger = logger1;
        }

        public void OnGet()
        {
        }

        //async - dnt need to wait for the request to come; is like a handphne [both sides]
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var protector = _dataProtectionProvider.CreateProtector("MySecretKey");
                if (RModel.Resume != null)
                {
                    var resumeFolder = "resumes";
                    var filename = Path.GetFileName(RModel.Resume.FileName);
                    var resumeFile = Guid.NewGuid() + Path.GetExtension(
                    filename);
                    var resumePath = Path.Combine(_environment.ContentRootPath,
                    "wwwroot", resumeFolder, resumeFile);
                    using var fileStream = new FileStream(resumePath, FileMode.Create);
                    await RModel.Resume.CopyToAsync(fileStream);
                    logger.LogInformation(string.Format("/{0}/{1}", resumeFolder, resumeFile));

                    var user = new ApplicationUser()
                    {
                        Fname = RModel.Fname,
                        Lname = RModel.Lname,
                        Gender = RModel.Gender,
                        NRIC = protector.Protect(RModel.NRIC),
                        Email = RModel.Email,
                        UserName = RModel.Email,
                        BirthDate = RModel.BirthDate,
                        ResumeURL = string.Format("/{0}/{1}", resumeFolder, resumeFile),
                        WhoamI = RModel.WhoamI,
                    };
                    ////Create the Admin role if NOT exist
                    //// roleManager.FindByIdAsync("Admin") – attempts to search for the “Admin” role in AspNetRoles table.
                    //IdentityRole role = await roleManager.FindByIdAsync("Admin");
                    //if (role == null)
                    //{
                    //    // roleManager.CreateAsync(new IdentityRole("Admin") – Create a new “Admin” role and add into AspNetRoles table.
                    //    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    //    if (!result2.Succeeded)
                    //    {
                    //        ModelState.AddModelError("", "Create role admin failed");
                    //    }
                    //}
                    var result = await userManager.CreateAsync(user, RModel.Password);
                    if (result.Succeeded)
                    {
                        ////Add users to Admin Role
                        //result = await userManager.AddToRoleAsync(user, "Admin");

                        await signInManager.SignInAsync(user, false);
                        return RedirectToPage("/Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                };
            }
            return Page();
        }
    }
}
