using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Models.RequestModels
{
    public class UserChangePasswordModel
    {
        public string userId { get; set; }
        public string CurPass { get; set; }
        public string NewPass { get; set; }
    }
}
