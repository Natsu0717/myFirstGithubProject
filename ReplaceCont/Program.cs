using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceCont
{
    class Program
    {
        static void Main(string[] args)
        {


            List<string> a = new List<string>() { "1", "2", "3" };
            List<string> aa = new List<string>();
            foreach (var item in a)
            {
                var tem = "'" + item + "'";
                aa.Add(tem);
            }

            string b = string.Join(",", aa);
           
            


            string aaa = "\"3333\"";

            string bbb = aaa.Replace("\"", "\'");
        }
    }
}
