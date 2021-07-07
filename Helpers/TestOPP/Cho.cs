using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Helpers.TestOPP
{
    public class Cho : DongVat
    {
        public Cho()
        {
            this.Cao = 1;
            this.Nang = 1;
        }
        public Cho(int h, int w) : base(h, w)
        {
            this.Cao = h;
            this.Nang = w;
        }

        public override void Keu()
        {
            Console.WriteLine("Cho keu!");
            Console.WriteLine("Cao: " + this.Cao);
            Console.WriteLine("Nang: " + this.Nang);
        }
    }
}
