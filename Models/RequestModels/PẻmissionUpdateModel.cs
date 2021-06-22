using System.Collections.Generic;
using WebBase.Models.ViewModels;

namespace WebBase.Models.RequestModels
{
    public class PẻmissionUpdateModel
    {
        public List<PermissionVM> Permissions { get; set; } = new List<PermissionVM>();
    }
}