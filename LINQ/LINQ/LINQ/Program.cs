using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lotteryNums = { 36, 66, 12, 6, 21, 3 };
            Print("original nums", lotteryNums);
            // output all nums
            var allNums = from n in lotteryNums
                       select n;
            Print("All nums", allNums);

            // ascending order
            var ascNums = from n in lotteryNums
                          orderby n ascending
                          select n;
            Print("Ascending nums", ascNums);

            // squares
            var squNums = from n in lotteryNums
                          select n * n;
            Print("Squared nums", squNums);

            // odd nums
            var oddNums = from n in lotteryNums
                          where n % 2 != 0
                          select n;
            Print("Odd nums", oddNums);

            // sum
            var sum = (from n in lotteryNums
                       select n).Sum();
            Console.WriteLine($"SUM: {sum}");
        }

        static void Print(string label, IEnumerable<int> nums)
        {
            Console.WriteLine($"-{label}-");
            foreach (var num in nums)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine("------");
        }
    }
}
