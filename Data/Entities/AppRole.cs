using Microsoft.AspNetCore.Identity;

namespace WebBase.Data.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}