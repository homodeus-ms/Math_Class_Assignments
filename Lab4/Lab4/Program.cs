using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("------------------------------Remove()------------------------------------------------------");

            var set1 = new MultiSet();
            set1.Add("b");
            set1.Add("a");
            Console.WriteLine("\n{ b, a }");
            Console.WriteLine("MultiSet.Remove(\"a\") => { b }");
            Debug.Assert(set1.Remove("a"));
            Debug.Assert(set1.GetMultiplicity("a") == 0);

            set1.Add("a");
            set1.Add("c");
            set1.Add("a");
            Console.WriteLine("\n{ a, c, a }");
            Console.WriteLine("MultiSet.Remove(\"d\") => { a, c, a }\n");
            Debug.Assert(set1.GetMultiplicity("a") == 2);
            Debug.Assert(!set1.Remove("d"));

            Console.WriteLine("------------------------------ToList()------------------------------------------------------");

            var expectedList = new List<string> { "a", "a", "b", "c" };
            var list = set1.ToList();

            Console.WriteLine("\n{ a, a, b, c }");
            Console.WriteLine("MultiSet.ToList() => { a, a, b, c }");
            EqualList(expectedList, list);

            Console.WriteLine("MultiSet.Remove(\"a\") => { a, b, c }");
            Debug.Assert(set1.Remove("a"));
            expectedList = new List<string> { "a", "b", "c" };
            Console.WriteLine("MultiSet.ToList() => { a, b, c }");
            list = set1.ToList();
            EqualList(expectedList, list);

            expectedList = new List<string>();
            Console.WriteLine("\n{   }");
            Console.WriteLine("MultiSet.ToList() => {   }\n");
            EqualList(expectedList, list);

            Console.WriteLine("------------------------------Union()-------------------------------------------------------");

            set1 = new MultiSet();
            set1.Add("a");
            set1.Add("b");
            set1.Add("b");
            set1.Add("c");

            var set2 = new MultiSet();
            set2.Add("a");
            set2.Add("b");
            set2.Add("c");

            list = set1.Union(set2).ToList();

            var expectedSet1 = new List<string> { "a", "b", "b", "c" };
            var expectedSet2 = new List<string> { "a", "b", "c" };
            expectedList = new List<string> { "a", "b", "b", "c" };

            Console.WriteLine("\n{ a, b, b, c } | { a, b, c }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Union(other) => { a, b, b, c }");
            EqualList(expectedList, list);

            set1 = new MultiSet();
            set1.Add("A");
            set1.Add("C");
            set1.Add("B");

            list = set1.Union(set2).ToList();
            Console.WriteLine($"list ordered : {string.Join(" ", list)}");

            expectedSet1 = new List<string> { "A", "B", "C" };
            expectedSet2 = new List<string> { "a", "b", "c" };
            expectedList = new List<string> { "a", "A", "b", "B", "c", "C" };

            Console.WriteLine("\n\n{ A, B, C } | { a, b, c }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Union(other) => { a, A, b, B, c, C }");
            EqualList(expectedList, list);

            set2 = new MultiSet();
            list = set1.Union(set2).ToList();
            expectedSet1 = new List<string> { "A", "B", "C" };
            expectedSet2.Clear();
            expectedList = new List<string> { "A", "B", "C" };

            Console.WriteLine("\n{ A, B, C } | {   }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Union(other) => { A, B, C }");
            EqualList(expectedList, list);

            set1 = new MultiSet();
            list = set1.Union(set2).ToList();
            expectedSet1.Clear();
            expectedList.Clear();

            Console.WriteLine("\n{   } | {   }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Union(other) => {   }\n");
            EqualList(expectedList, list);

            Console.WriteLine("------------------------------Intersect()---------------------------------------------------");

            set1 = new MultiSet();
            set1.Add("a");
            set1.Add("b");
            set1.Add("b");
            set1.Add("c");

            set2 = new MultiSet();
            set2.Add("a");
            set2.Add("b");
            set2.Add("c");

            list = set1.Intersect(set2).ToList();
            expectedSet1 = new List<string> { "a", "b", "b", "c" };
            expectedSet2 = new List<string> { "a", "b", "c" };
            expectedList = new List<string> { "a", "b", "c" };

            Console.WriteLine("\n{ a, b, b, c } | { a, b, c }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Intersect(other) => { a, b, c }");
            EqualList(expectedList, list);

            set1 = new MultiSet();
            set1.Add("A");
            set1.Add("C");
            set1.Add("B");

            list = set1.Intersect(set2).ToList();
            expectedSet1 = new List<string> { "A", "B", "C" };
            expectedSet2 = new List<string> { "a", "b", "c" };
            expectedList.Clear();

            Console.WriteLine("\n{ A, B, C } | { a, b, c }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Intersect(other) => {   }");
            EqualList(expectedList, list);

            set2 = new MultiSet();
            list = set1.Intersect(set2).ToList();
            expectedSet1 = new List<string> { "A", "B", "C" };
            expectedSet2.Clear();

            Console.WriteLine("\n{ A, B, C } | {   }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Intersect(other) => { A, B, C }");
            EqualList(expectedList, list);

            set1 = new MultiSet();
            list = set1.Intersect(set2).ToList();
            expectedSet1.Clear();
            expectedList.Clear();

            Console.WriteLine("\n{   } | {   }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Intersect(other) => {   }\n");
            EqualList(expectedList, list);

            Console.WriteLine("------------------------------Subtract()----------------------------------------------------");

            set1 = new MultiSet();

            set1.Add("d");
            set1.Add("k");
            set1.Add("f");
            set1.Add("e");
            set1.Add("e");

            set2 = new MultiSet();
            set2.Add("g");
            set2.Add("e");
            set2.Add("f");

            list = set1.Subtract(set2).ToList();
            expectedSet1 = new List<string> { "d", "e", "e", "f", "k" };
            expectedSet2 = new List<string> { "e", "f", "g" };
            expectedList = new List<string> { "d", "e", "k" };

            Console.WriteLine("\n{ d, e, e, f, k } | { e, f, g }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Subtract(other) => { d, e, k }");
            EqualList(expectedList, list);

            set1 = new MultiSet();
            set1.Add("d");
            set1.Add("k");
            set1.Add("f");
            set1.Add("e");

            set2 = new MultiSet();
            set2.Add("g");
            set2.Add("e");
            set2.Add("f");
            set2.Add("e");

            list = set1.Subtract(set2).ToList();
            expectedSet1 = new List<string> { "d", "e", "f", "k" };
            expectedSet2 = new List<string> { "e", "e", "f", "g" };
            expectedList = new List<string> { "d", "k" };

            Console.WriteLine("\n{ d, e, f, k } | { e, e, f, g }");
            EqualList(expectedSet1, set1.ToList());
            EqualList(expectedSet2, set2.ToList());
            Console.WriteLine("MultiSet.Subtract(other) => { d, k }\n");
            EqualList(expectedList, list);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"set2 : {string.Join(", ", set2.Set)}");
            Console.WriteLine($"set1 : {string.Join(", ", set1.Set)}");

            Console.WriteLine($"set2.IsSubsetOf(set1) : {set2.IsSubsetOf(set1)}");
            Console.WriteLine($"set2.IsSupersetOf(set1) : {set2.IsSupersetOf(set1)}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            Debug.Assert(set2.IsSubsetOf(set1));
            Debug.Assert(!set2.IsSupersetOf(set1));
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
