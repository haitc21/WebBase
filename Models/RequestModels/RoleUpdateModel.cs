using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Models.RequestModels
{
    public class RoleUpdateModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
