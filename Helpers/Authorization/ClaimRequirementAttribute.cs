using Microsoft.AspNetCore.Mvc;
using static WebBase.Common.Enums;

namespace WebBase.Helpers.Authorization
{
    /// <summary>
    /// Tao ra atribute kiem tra quyen cuar tung api
    /// </summary>
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(FunctionCode functionId, CommandCode commandId)
            : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { functionId, commandId };
        }
    }
}