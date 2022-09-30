using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------------- check : Add, Remove, GetMultiplicity -----------------------------------
            
            List<string> list1 = new List<string> { "aa", "a", "Z", "가", "a", " ", "ball", "Apple", "a"};

            MultiSet set1 = new MultiSet();

            AddElement(set1, list1);
            
            List<string> expected = new List<string> { " ", "Apple", "Z", "a", "a", "a", "aa", "ball", "가" };
            
            List<string> yourList = set1.ToList();

            Debug.Assert(IsSameList(expected, yourList));

            Debug.Assert(set1.Remove("aa"));
            Debug.Assert(!set1.Remove("c"));
            Debug.Assert(set1.Remove("가"));
            Debug.Assert(!set1.Remove("가"));

            expected = new List<string> { " ", "Apple", "Z", "a", "a", "a", "ball" };

            yourList = set1.ToList();

            Debug.Assert(IsSameList(expected, yourList));

            Debug.Assert(set1.GetMultiplicity("a") == 3);
            Debug.Assert(set1.GetMultiplicity("가") == 0);
            Debug.Assert(set1.GetMultiplicity("Ball") == 0);
            Debug.Assert(set1.GetMultiplicity("Apple") == 1);
            Debug.Assert(set1.GetMultiplicity(" ") == 1);

            set1 = new MultiSet();

            Debug.Assert(set1.GetMultiplicity("a") == 0);

            for (int i = 0; i < 15; ++i)
            {
                set1.Add("a");
            }

            Debug.Assert(set1.GetMultiplicity("a") == 15);



            // ----------------------------------------- check : ToList() ------------------------------------------

            set1 = new MultiSet();

            list1 = new List<string> { "A", "a", "zz", "Z", " ", "C", "1", "ㄱ", "ㄴ", "ㄱㄴ", "apple", "Aapple", "a", "aa", "aa" };

            AddElement(set1, list1);

            yourList = set1.ToList();

            expected = new List<string> { " ", "1", "A", "Aapple", "C", "Z", "a", "a", "aa", "aa", "apple", "zz", "ㄱ", "ㄱㄴ", "ㄴ" };

            Debug.Assert(IsSameList(expected, yourList));


            // ------------------------------------ Check : Union, Intersect, Subtract -------------------------------

            //현재 set1의 원소 = { " ", "1", "A", "Aapple", "C", "Z", "a", "a", "aa", "aa", "apple", "zz", "ㄱ", "ㄱㄴ", "ㄴ"} 
            //한쪽 중복집합이 비어있는 경우

            MultiSet set2 = new MultiSet();

            MultiSet keep1 = KeepOriginSet(set1);
            MultiSet keep2 = KeepOriginSet(set2);

            MultiSet resultSet = set1.Union(set2);
            Debug.Assert(IsSameMultiSet(set1, resultSet));
            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            resultSet = set2.Union(set1);
            Debug.Assert(IsSameMultiSet(set1, resultSet));

            resultSet = set1.Intersect(set2);
            Debug.Assert(IsSameMultiSet(new MultiSet(), resultSet));
            resultSet = set2.Intersect(set1);
            Debug.Assert(IsSameMultiSet(new MultiSet(), resultSet));

            resultSet = set1.Subtract(set2);
            Debug.Assert(IsSameMultiSet(set1, resultSet));
            resultSet = set2.Subtract(set1);
            Debug.Assert(IsSameMultiSet(new MultiSet(), resultSet));

            // 중복된 원소가 있는 경우

            list1 = new List<string> { " ", "a", "a", "a", "a", "aa" };

            AddElement(set2, list1);

            expected = new List<string> 
            {
                " ", "1", "A", "Aapple", "C", "Z", "a", "a", "a", "a", "aa", "aa", "apple", "zz", "ㄱ", "ㄱㄴ", "ㄴ" 
            };

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            yourList = set1.Union(set2).ToList();
            Debug.Assert(IsSameList(yourList, expected));
            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            yourList = set2.Union(set1).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            expected = new List<string>
            {
                " ", "a", "a", "aa"
            };

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            yourList = set1.Intersect(set2).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            yourList = set2.Intersect(set1).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            expected = new List<string>
            {
                "1", "A", "Aapple", "C", "Z", "aa", "apple", "zz", "ㄱ", "ㄱㄴ", "ㄴ"
            };

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            yourList = set1.Subtract(set2).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            expected = new List<string>
            {
                "a", "a"
            };

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            yourList = set2.Subtract(set1).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));


            //중복된 원소가 없는 경우

            set1 = new MultiSet();
            set2 = new MultiSet();
            list1 = new List<string> { "a", "aa", "aaa", "aaaa" };
            var list2 = new List<string> { "A", "AA", "Aaa", "Aaaa" };

            AddElement(set1, list1);
            AddElement(set2, list2);

            expected = new List<string>
            {
                "A", "AA", "Aaa", "Aaaa", "a", "aa", "aaa", "aaaa"
            };

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            yourList = set1.Union(set2).ToList();
            Debug.Assert(IsSameList(yourList, expected));
            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            yourList = set2.Union(set1).ToList();
            Debug.Assert(IsSameList(yourList, expected));

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            resultSet = set1.Intersect(set2);
            Debug.Assert(IsSameMultiSet(new MultiSet(), resultSet));
            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            resultSet = set2.Intersect(set1);
            Debug.Assert(IsSameMultiSet(new MultiSet(), resultSet));

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            resultSet = set1.Subtract(set2);
            Debug.Assert(IsSameMultiSet(set1, resultSet));
            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            resultSet = set2.Subtract(set1);
            Debug.Assert(IsSameMultiSet(set2, resultSet));


            // 두 집합이 다 비어있는 경우

            set1 = new MultiSet();
            set2 = new MultiSet();

            keep1 = KeepOriginSet(set1);
            keep2 = KeepOriginSet(set2);

            Debug.Assert(IsSameMultiSet(set1, set1.Union(set2)));
            Debug.Assert(IsSameMultiSet(set2, set1.Union(set1)));
            Debug.Assert(IsSameMultiSet(set1, set1.Intersect(set2)));
            Debug.Assert(IsSameMultiSet(set2, set1.Intersect(set1)));
            Debug.Assert(IsSameMultiSet(set1, set1.Subtract(set2)));
            Debug.Assert(IsSameMultiSet(set2, set1.Subtract(set1)));

            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));

            Debug.Assert(IsSameMultiSet(new MultiSet(), set1.Union(set2)));
            Debug.Assert(IsSameMultiSet(new MultiSet(), set1.Intersect(set2)));
            Debug.Assert(IsSameMultiSet(new MultiSet(), set2.Intersect(set1)));
            Debug.Assert(IsSameMultiSet(new MultiSet(), set1.Subtract(set2)));
            Debug.Assert(IsSameMultiSet(new MultiSet(), set2.Subtract(set1)));

            Debug.Assert(IsSameMultiSet(keep1, set1));
            Debug.Assert(IsSameMultiSet(keep2, set2));


            //-------------------------------------- check : PowerSet -------------------------------------------

            // 비어있는 멀티셋

            set1 = new MultiSet();
            Debug.Assert(set1.ToList().Count == 0);

            List<MultiSet> powerSets = set1.FindPowerSet();

            Debug.Assert(powerSets.Count == 1);
            Debug.Assert(powerSets[0].ToList().Count == 0);
            
            list1 = new List<string> { "a", "b", "c", "d"};
            AddElement(set1, list1);

            powerSets = set1.FindPowerSet();
            Debug.Assert(powerSets.Count == 16);
            PrintPowerSets(powerSets);

            set1.Remove("d");
            set1.Add("a");

            powerSets = set1.FindPowerSet();
            Debug.Assert(powerSets.Count == 12);
            PrintPowerSets(powerSets);

            set1.Add("b");

            powerSets = set1.FindPowerSet();
            Debug.Assert(powerSets.Count == 18);
            PrintPowerSets(powerSets);

            set1 = new MultiSet();
            
            for (int i = 0; i < 10; ++i)
            {
                set1.Add("a");
            }

            powerSets = set1.FindPowerSet();
            Debug.Assert(powerSets.Count == 11);
            PrintPowerSets(powerSets);


            //---------------------------------- chekc : subSet, superSet --------------------------------------

            set1 = new MultiSet();
            set2 = new MultiSet();
            list1 = new List<string> { "a", "b" };
            list2 = new List<string> { "a", "b", "aa", "b", "c" };

            Debug.Assert(set1.IsSubsetOf(set2));
            Debug.Assert(set2.IsSubsetOf(set1));
            Debug.Assert(set1.IsSupersetOf(set2));
            Debug.Assert(set2.IsSupersetOf(set1));

            AddElement(set1, list1);

            Debug.Assert(!set1.IsSubsetOf(set2));
            Debug.Assert(set2.IsSubsetOf(set1));
            Debug.Assert(set1.IsSupersetOf(set2));
            Debug.Assert(!set2.IsSupersetOf(set1));

            AddElement(set2, list2);

            Debug.Assert(set1.IsSubsetOf(set2));
            Debug.Assert(!set2.IsSubsetOf(set1));
            Debug.Assert(!set1.IsSupersetOf(set2));
            Debug.Assert(set2.IsSupersetOf(set1));

            set1.Add("z");

            Debug.Assert(!set1.IsSubsetOf(set2));
            Debug.Assert(!set2.IsSubsetOf(set1));
            Debug.Assert(!set1.IsSupersetOf(set2));
            Debug.Assert(!set2.IsSupersetOf(set1));

            set1 = new MultiSet();
            set2 = new MultiSet();

            list1 = new List<string> { "a", "aa", "aaa" };
            list2 = new List<string> { "A", "AA", "AAA" };

            AddElement(set1, list1);
            AddElement(set2, list2);

            Debug.Assert(!set1.IsSubsetOf(set2));
            Debug.Assert(!set2.IsSubsetOf(set1));
            Debug.Assert(!set1.IsSupersetOf(set2));
            Debug.Assert(!set2.IsSupersetOf(set1));

        }
        public static void AddElement(MultiSet set, List<string> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                set.Add(list[i]);
            }

        }
        public static bool IsSameList(List<string> expectedList, List<string> list)
        {
            bool bEqual = true;
            
            if (expectedList.Count != list.Count)
            {
                return false;
            }

            for (int i = 0; i < expectedList.Count; i++)
            {
                if (expectedList[i] != list[i])
                {
                    bEqual = false;
                    break;
                }
            }

            return bEqual;
        }
        
        public static bool IsSameMultiSet(MultiSet set1, MultiSet set2)
        {
            bool bEqual = true;
            
            var set1List = set1.ToList();
            var set2List = set2.ToList();

            if (set1List.Count != set2List.Count)
            {
                return false;
            }

            for (int i = 0; i < set1List.Count; i++)
            {
                if (set1List[i] != set2List[i])
                {
                    bEqual = false;
                    break;
                }
            }

            return bEqual;
        }
        public static MultiSet KeepOriginSet(MultiSet set1)
        {
            MultiSet keepSet = new MultiSet();
            List<string> elements = set1.ToList();

            for (int i = 0; i < elements.Count; ++i)
            {
                keepSet.Add(elements[i]);
            }

            return keepSet;
        }
        public static void PrintPowerSets(List<MultiSet> powerSets)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"********************** PowerSet Count : {powerSets.Count} *************************");
            Console.WriteLine();

            for (int i = 0; i < powerSets.Count; ++i)
            {
                var temp = powerSets[i].ToList();

                Console.WriteLine($"set[{i}] : ( {string.Join(", ", temp)} )");
            }
            Console.WriteLine();
            Console.WriteLine();

        }
        
    }
}

