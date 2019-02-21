using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuiNiu
{
    public class A : Virtual
    {
        public override void Print()
        {
            Console.WriteLine("This is A,Override by Virtual.");
        }

    }

    public class B : Virtual
    {
        public new void Print()
        {
            Console.WriteLine("This is B,New by Virtual.");
        }
    }

    public class C : Abstract
    {
        public override void Print()
        {
            Console.WriteLine("This is C,Override by Abstract. ");
        }
        public C()
        {
            AbstractProp1 = 1;
            AbstractProp2 = "2";
        }
    }

    public class D : Interface1
    {
      
        public void Print()
        {
            Console.WriteLine("This is a Interface");
        }
    }
}
