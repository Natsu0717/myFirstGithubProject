using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ExHelper ex = new ExHelper();
            var ss = Convert.ToDateTime("2019/4/15 12:24");
            ex.ExChangeMail();
        }
    }
}
