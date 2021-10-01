using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Models.ViewModels
{
    public class UserToleVM
    {
        public UserToleVM(List<string> role, List<string> roleNot)
        {
            this.Roles = role;
            this.roleNotHas = roleNot;
        }
        public List<string> Roles { set; get; }
        public List<string> roleNotHas { set; get; }
    }
}
