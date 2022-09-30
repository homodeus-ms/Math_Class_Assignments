using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public static class Support
    {
        public static List<string> OrderList(List<string> list)
        {
            List<string> result = new List<string>();
            
            orderListRecursive(list, 0, list.Count - 1);

            for (int i = 0; i < list.Count; i++)
            {
                result.Add(list[i]);
            }

            return result;
        }
        public static void GetPowerSetsRecursive(List<string> list, List<MultiSet> result, int index)
        {
            if (index == 0)
            {
                result.Add(new MultiSet());

                MultiSet temp = new MultiSet();

                temp.Add(list[index]);

                result.Add(temp);

                return;
            }

            GetPowerSetsRecursive(list, result, index - 1);

            int end = (int)Math.Pow(2, index);

            for (int i = 0; i < end; ++i)
            {
                MultiSet tempSet = new MultiSet();

                tempSet.Elements.AddRange(result[i].Elements);

                tempSet.Add(list[index]);

                result.Add(tempSet);
            }
        }
        public static void DeleteDuplicates(List<MultiSet> powerSet)
        {
            for (int i = 0; i < powerSet.Count - 1; ++i)
            {
                List<string> basisList = powerSet[i].Elements;

                for (int j = i + 1; j < powerSet.Count; ++j)
                {
                    if (basisList.Count != powerSet[j].Elements.Count)
                    {
                        continue;
                    }

                    if (IsSameList(basisList, powerSet[j].Elements))
                    {
                        powerSet.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
        public static bool IsSameList(List<string> listA, List<string> listB)
        {
            if (listA.Count != listB.Count)
            {
                return false;
            }    
            
            for (int i = 0; i < listA.Count; ++i)
            {
                if (listA[i] != listB[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static void orderListRecursive(List<string> list, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int pivotPos = getPivotPos(list, left, right);

            orderListRecursive(list, left, pivotPos - 1);
            orderListRecursive(list, pivotPos + 1, right);

        }
        private static int getPivotPos(List<string> list, int left, int right)
        {
            string pivot = list[right];

            if (string.IsNullOrEmpty(pivot))
            {
                swap(list, left, right);
                return 0;
            }

            byte[] pivotBytes = Encoding.UTF8.GetBytes(pivot);

            for (int i = left; i < right; ++i)
            {
                if (string.IsNullOrEmpty(list[i]))
                {
                    swap(list, i, left);
                    left++;
                    continue;
                }

                byte[] elementBytes = Encoding.UTF8.GetBytes(list[i]);

                int shorterByteLength = elementBytes.Length < pivotBytes.Length ? elementBytes.Length : pivotBytes.Length;

                for (int j = 0; j < shorterByteLength; ++j)
                {
                    if (elementBytes[j] < pivotBytes[j])
                    {
                        swap(list, i, left);
                        left++;
                        break;
                    }

                    if (elementBytes[j] == pivotBytes[j])
                    {
                        if (j == shorterByteLength - 1 && shorterByteLength == elementBytes.Length)
                        {
                            swap(list, i, left);
                            left++;
                            break;
                        }

                        continue;
                    }

                    break;
                }
            }

            int pivotPos = left;
            swap(list, left, right);

            return pivotPos;
        }
        private static void swap(List<string> list, int i, int j)
        {
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    
}
