using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Net;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiSet set1 = new MultiSet();
            MultiSet set2 = new MultiSet();
            MultiSet set3 = new MultiSet();

            List<string> set1List = new List<string> { " ", "", "a", "  " };
            List<string> set2List = new List<string> { "z", "c", "e", "a", "ZZ" };
            List<string> set3List = new List<string> { "다나가", "갉", "가나", "라디오", "ㄱ", "ㄹ", "가가가", "나" };

            for (int i = 0; i < set1List.Count; i++)
            {
                set1.Add(set1List[i]);
            }

            set1.ToList();

            Console.WriteLine($"set1 : {string.Join(", ", set1.Set)}");

            /*
            for (int i = 0; i < set1List.Count; i++)
            {
                set1.Add(set1List[i]);
            }
            for (int i = 0; i < set2List.Count; i++)
            {
                set2.Add(set2List[i]);
            }
            
            Console.WriteLine($"set1 : {string.Join(", ", set1.Set)}");
            Console.WriteLine($"set2 : {string.Join(", ", set2.Set)}");

            Console.WriteLine("---------------------------------------------");

            var result = set2.Subtract(set1);

            Console.WriteLine($"Subtract : {string.Join(", ", result.Set)}");
            Console.WriteLine($"set1 : {string.Join(", ", set1.Set)}");
            Console.WriteLine($"set2 : {string.Join(", ", set2.Set)}");
            */
        }
    }
}