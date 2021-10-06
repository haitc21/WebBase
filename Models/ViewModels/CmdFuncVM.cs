using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Models.ViewModels
{
    public class CmdFuncVM
    {
        public CmdFuncVM(List<CommandVM> cmdIn, List<CommandVM> cmdNotIn)
        {
            this.CmdInFunc = cmdIn;
            this.CmdNotInFunc = cmdNotIn;
        }
        public  List<CommandVM> CmdInFunc { get; set; }
        public List<CommandVM> CmdNotInFunc { get; set; }
    }
}
