using Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class AppUser : IdentityUser<Guid>, IDateTracking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
