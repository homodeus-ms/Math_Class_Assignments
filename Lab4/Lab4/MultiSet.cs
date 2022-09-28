using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public sealed class MultiSet
    {
        public List<string> StrList { get; private set; } = new List<string>();
        public string ConcatString { get; private set; }

        private uint mCount;

        public void Add(string element)
        {
            StrList.Add(element);
        }

        public bool Remove(string element)
        {
            return StrList.Remove(element);
        }

        public uint GetMultiplicity(string element)
        {
            mCount = 0;

            for (int i = 0; i < StrList.Count; ++i)
            {
                if (StrList[i] == element)
                {
                    mCount++;
                }
            }

            return mCount == 0 ? 0 : mCount;
        }

        public List<string> ToList()
        {
            if (StrList.Count == 0 || StrList == null)
            {
                return new List<string>();
            }
            
            sortSetRecursive(0, StrList.Count - 1);

            List<string> result = new List<string>();

            for (int i = 0; i < StrList.Count; i++)
            {
                result.Add(StrList[i]);
            }

            return result;
        }

        public MultiSet Union(MultiSet other)
        {
            if (StrList.Count == 0 && other.StrList.Count == 0)
            {
                return new MultiSet();
            }

            MultiSet result = new MultiSet();

            result.StrList.AddRange(StrList);

            for (int i = 0; i < other.StrList.Count; i++)
            {
                if (result.StrList.Contains(other.StrList[i]) && result.GetMultiplicity(other.StrList[i]) >= other.GetMultiplicity(other.StrList[i]))
                {
                    continue;
                }
                else
                {
                    result.StrList.Add(other.StrList[i]);
                }
            }

            this.ToList();
            other.ToList();
            result.ToList();

            return result;
        }

        public MultiSet Intersect(MultiSet other)
        {
            if (StrList.Count == 0 && other.StrList.Count == 0)
            {
                return new MultiSet();
            }

            MultiSet result = new MultiSet();

            for (int i = 0; i < StrList.Count; i++)
            {
                if (other.StrList.Contains(StrList[i]))
                {
                    if (!result.StrList.Contains(StrList[i]))
                    {
                        result.StrList.Add(StrList[i]);
                        continue;
                    }

                    uint thisMultiplCount = GetMultiplicity(StrList[i]);
                    uint otherMultiplCount = other.GetMultiplicity(StrList[i]);

                    uint lessMultiplCount = thisMultiplCount < otherMultiplCount ? thisMultiplCount : otherMultiplCount;

                    if (lessMultiplCount > result.GetMultiplicity(StrList[i]))
                    {
                        result.StrList.Add(StrList[i]);
                    }
                }
            }

            this.ToList();
            other.ToList();
            result.ToList();

            return result;
        }

        public MultiSet Subtract(MultiSet other)
        {
            if (StrList.Count == 0 && other.StrList.Count == 0)
            {
                return new MultiSet();
            }

            MultiSet result = new MultiSet();

            result.StrList.AddRange(StrList);
            
            for (int i = 0; i < other.StrList.Count; i++)
            {
                for (int j = 0; j < result.StrList.Count; j++)
                {
                    if (other.StrList[i] == result.StrList[j])
                    {
                        result.StrList.RemoveAt(j);
                        break;
                    }
                }
            }

            this.ToList();
            other.ToList();
            result.ToList();

            return result;
        }

        public List<MultiSet> FindPowerSet()
        {
            List<MultiSet> powerSets = new List<MultiSet>();

            powerSets.Add(new MultiSet());

            int count = 1;

            for (int i = 0; i < StrList.Count; ++i)
            {
                string temp = StrList[i];

                for (int j = 0; j < powerSets.Count; ++j)
                {
                    MultiSet addedSet = new MultiSet();
                    addedSet.StrList.AddRange(powerSets[j].StrList);
                    addedSet.Add(temp);
                    addedSet.ToList();

                    powerSets.Add(addedSet);

                    if (powerSets.Count == (int)Math.Pow(2, count))
                    {
                        break;
                    }
                }

                count++;
            }

            deleteDuplicates(powerSets);

            powerSets = orderMultiSets(powerSets);

            return powerSets;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            if (StrList.Count == 0)
            {
                return true;
            }
            
            MultiSet intersect = new MultiSet();
            intersect.StrList.AddRange(StrList);
            intersect = intersect.Intersect(other);

            if (isSameMultiSet(intersect, this))
            {
                return true;
            }

            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            if (other.StrList.Count == 0 || other == null)
            {
                return true;
            }

            MultiSet intersect = new MultiSet();
            intersect.StrList.AddRange(StrList);
            intersect = intersect.Intersect(other);

            if (isSameMultiSet(intersect, other))
            {
                return true;
            }

            return false;
        }
        private void getConcatString()
        {
            ConcatString = string.Join("", StrList);
        }
        private void sortSetRecursive(int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int pivotPos = getPivotPos(left, right);

            sortSetRecursive(left, pivotPos - 1);
            sortSetRecursive(pivotPos + 1, right);

        }
        private int getPivotPos(int left, int right)
        {
            string pivot = StrList[right];
            //Encoding encoding = new UTF32Encoding(true, true);

            for (int i = left; i < right; ++i)
            {
                if (StrList[i].Length == 0)
                {
                    swapSetContents(i, left);
                    left++;
                    continue;
                }

                byte[] bytesLeft = Encoding.UTF8.GetBytes(StrList[i]);
                byte[] bytesRight = Encoding.UTF8.GetBytes(pivot);
                
                int shortLength = bytesLeft.Length < bytesRight.Length ? bytesLeft.Length : bytesRight.Length;

                for (int j = 0; j < shortLength; ++j)
                {
                    if (bytesLeft[j] < bytesRight[j])
                    {
                        swapSetContents(i, left);
                        left++;
                        break;
                    }

                    else if (bytesLeft[j] == bytesRight[j])
                    {
                        if (j == shortLength - 1 && bytesLeft.Length < bytesRight.Length)
                        {
                            swapSetContents(i, left);
                            left++;
                            break;
                        }

                        continue;
                    }
                        
                    break;
                }
            }

            swapSetContents(left, right);

            return left;

        }
        private void swapSetContents(int i, int j)
        {
            string temp = StrList[i];
            StrList[i] = StrList[j];
            StrList[j] = temp;
        }
        private void deleteDuplicates(List<MultiSet> powerSets)
        {
            for (int i = 0; i < powerSets.Count - 1; ++i)
            {
                MultiSet basis = powerSets[i];

                for (int j = i + 1; j < powerSets.Count; ++j)
                {
                    if (isSameMultiSet(basis, powerSets[j]))
                    {
                        powerSets.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
        
        private List<MultiSet> orderMultiSets(List<MultiSet> setLists)
        {
            for (int i = 0; i < setLists.Count; i++)
            {
                setLists[i].getConcatString();
            }

            setLists = setLists.OrderBy(x => x.ConcatString).ToList();

            return setLists;
        }
        private bool isSameMultiSet(MultiSet set1, MultiSet set2)
        {
            if (set1.StrList == null && set2.StrList == null)
            {
                return true;
            }

            if (set1.StrList.Count != set2.StrList.Count)
            {
                return false;
            }
            
            set1.ToList();
            set2.ToList();
            
            for (int i = 0; i < set1.StrList.Count; i++)
            {
                if (set1.StrList[i] != set2.StrList[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
