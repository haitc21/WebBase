using Microsoft.AspNetCore.Identity;
using System;
using WebBase.Data.Interfaces;

namespace WebBase.Data.Entities
{
    public class AppUser : IdentityUser<string>, IDateTracking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}