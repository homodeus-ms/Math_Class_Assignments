using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MultiSet set = new MultiSet();

            set.Add(" ");
            set.Add("A");
            set.Add("a");
            set.Add("apple");
            set.Add("가");
            set.Add("Ball");
            set.Add("가");
            set.Add("가나다");
            set.Add("7");
            set.Add("aAp");
            set.Add("Aapple");
            set.Add("ball");


            var result = set.ToList();

            Console.WriteLine(string.Join(", ", result));

            set.Add("가나다라");

            Console.WriteLine(string.Join(", ", result));

        }
        public static void EqualList(List<string> expectedList, List<string> list)
        {
            for (int i = 0; i < expectedList.Count; i++)
            {
                Debug.Assert(expectedList[i] == list[i]);
            }
        }
    }
}

