using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Helpers.TestOPP
{
    public abstract class MauSac : DongVat
    {
        public MauSac()
        {
            this.Cao = 2;
            this.Nang = 2;
        }
        public MauSac(int h, int w) : base(h, w)
        {
            this.Cao = h;
            this.Nang = w;
        }
        public string Colore { get; set; }

        public string SetMau(string color)
        {
            this.Colore = color;
            return color;
        }
        public abstract void NoMauGi();
    }
}
