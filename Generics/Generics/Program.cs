using System;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CycList<int> ints = new CycList<int>(5);
            ints.Add(7);
            ints.Add(36);
            ints.View();
            Console.WriteLine(ints.GetItem(1));
            ints.Add(3);
            ints.Add(2);
            ints.Add(1);
            ints.Add(100);
            ints.View();

            Console.WriteLine("Hello World!");
            CycList<string> strs = new CycList<string>(5);
            strs.Add("1");
            strs.Add("2");
            strs.View();
            Console.WriteLine(strs.GetItem(1));
            strs.Add("3");
            strs.Add("4");
            strs.Add("5");
            strs.Add("100");
            strs.View();
        }
    }
}
