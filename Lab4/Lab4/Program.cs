using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiSet set1 = new MultiSet();
            MultiSet set2 = new MultiSet();

            set1.ToList();
            var result = set1.Union(set2);
            Console.WriteLine(string.Join(", ", result));
            Console.WriteLine("--------------------------------------");
            result = set1.Intersect(set2);
            Console.WriteLine(string.Join(", ", result));

            Console.WriteLine("--------------------------------------");
            result = set1.Subtract(set2);
            Console.WriteLine(string.Join(", ", result));



            //set2.Add("");

            Console.WriteLine(set1.IsSubsetOf(set2));
            Console.WriteLine(set2.IsSubsetOf(set1));
            Console.WriteLine(set1.IsSupersetOf(set2));
            Console.WriteLine(set2.IsSupersetOf(set1));
        }
    }
}