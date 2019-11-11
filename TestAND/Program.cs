using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAND
{
    class Program
    {
        static void Main(string[] args)
        {
            AA aa = new AA();
            aa = null;
            if (aa != null && aa.AB != 0)
            {
                Console.WriteLine("1111");
                
            }
            Console.WriteLine("2222");
        }

        public class AA
        {
            public int AB { get; set; }
        }
    }
}
