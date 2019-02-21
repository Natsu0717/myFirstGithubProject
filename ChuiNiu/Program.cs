using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChuiNiu
{
    public class Program
    {
        static void Main(string[] args)
        {

            //A a = new A();
            //a.Print();

            //B b = new B();
            //b.Print();

            //Abstract abs = new C();
            //abs.Print();


            // Rex();
            Program p = new Program();
            Func func = new Func();
            //p.ActionTest(func.PrintString);
            p.FuncTest(func.CombineString);
            Console.ReadKey();
        }


        public void ActionTest(Action ac)
        {
            Console.WriteLine("调用无参数无返回值委托");
        }


        public void FuncTest(Func<string, string, string> fc)
        {
            var stringvalue = fc("y有参数", "有返回值");
            Console.WriteLine(stringvalue);
        }

        public static void Rex()
        {
            string str = "<You're angle & evil>";
            string pattern = "'|&|<|>";

            Regex regex = new Regex(pattern);

            //Program prog = new Program();
            MatchEvaluator evaluator = new MatchEvaluator(Program.ConvertToXML);
            Console.WriteLine(regex.Replace(str, evaluator));
            Console.Read();
        }


        public static string ConvertToXML(Match m)
        {
            switch (m.Value)
            {
                case "'":
                    return "&apos";
                case "&":
                    return "&amp";
                case "<":
                    return "&lt";
                case ">":
                    return "&gt";
                default:
                    return "";
            }
        }
    }

}




