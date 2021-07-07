using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBase.Helpers.TestOPP
{
    public class DongVat
    {
        public DongVat()
        {
            this.Cao = 0;
            this.Nang = 1;
        }
        public DongVat(int height, int weight)
        {
            this.Cao = height;
            this.Nang = weight;
        }
        public int Cao { get; set; }
        public int Nang { get; set; }

        public virtual void Keu()
        {
            Console.WriteLine("Dong vat keu!");
            Console.WriteLine("Cao: " + this.Cao);
            Console.WriteLine("Nang: " + this.Nang);
        }
    }
}
