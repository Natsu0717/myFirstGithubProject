
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubStringText
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "webwxgetm.sgimg.jpg";

            Console.WriteLine(text.Substring(text.IndexOf(".")));
            Console.ReadKey();
        }
    }
}
