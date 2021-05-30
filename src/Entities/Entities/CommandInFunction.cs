using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class CommandInFunction
    {
        public string CommandId { get; set; }

        public Command Command { get; set; }

        public string FunctionId { get; set; }

        public Function Function { get; set; }
    }
}
