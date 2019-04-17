using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuiNiu
{
    public class CtorTest
    {
        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public CtorTest(int a, int b)
        {
            C = a + b;
        }

        public CtorTest()
        {

        }
    }
}
