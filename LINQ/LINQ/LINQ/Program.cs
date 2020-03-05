using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {

            simpleCollectionExamples();
            objectCollectionExamples();
            joinsExamples();
        }

        static void joinsExamples()
        {
            Book[] books = { new Book("The BFG", "Whats his name", 12.99), new Book("The Bible", "12 disciples", 20.34) };
            Author[] authors = { new Author("Whats his name", 1947), new Author("12 disciples", 1) };

            var inner = from b in books
                        join a in authors on b.Author equals a.Name
                        select new { a.Name, a.BirthYear, b.Title };
            Print("Inner join", inner);
        }

        static void objectCollectionExamples()
        {
            // working with object collections
            BankAccount[] accounts = { new BankAccount("Sammy", 12.98),
                                       new BankAccount("Harry", 170.13),
                                       new BankAccount("Thomas", 55.70) };

            var allAccs = from a in accounts
                          select a;
            Print("All accounts", allAccs);

            // accounts with balance above 20
            var accs20 = from a in accounts
                         where a.Balance > 20
                         select a.AccountHolder;
            Print("Accounts above £20", accs20);
        }

        static void simpleCollectionExamples()
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

        static void Print<T>(string label, IEnumerable<T> items)
        {
            Console.WriteLine($"-{label}-");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------");
        }
    }
}
