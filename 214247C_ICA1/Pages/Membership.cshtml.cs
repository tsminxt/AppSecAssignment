using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _214247C_ICA1.Pages
{
    [Authorize(Policy ="Membership")]
    public class MembershipModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
