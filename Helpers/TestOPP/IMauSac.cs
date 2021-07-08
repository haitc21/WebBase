using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Helpers.TestOPP
{
    public interface IMauSac
    {
        public string Colore { get; set; }
        string SetMau(string color);
        void NoMauGi();
    }
}
