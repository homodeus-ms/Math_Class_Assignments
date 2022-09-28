using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    public sealed class MultiSet
    {
        public List<string> Set { get; private set; } = new List<string>();
        public string ConcatString { get; private set; }

        private uint mCount;

        public void Add(string element)
        {
            Set.Add(element);
        }

        public bool Remove(string element)
        {
            return Set.Remove(element);
        }

        public uint GetMultiplicity(string element)
        {
            mCount = 0;

            for (int i = 0; i < Set.Count; ++i)
            {
                if (Set[i] == element)
                {
                    mCount++;
                }
            }

            return mCount == 0 ? 0 : mCount;
        }

        public List<string> ToList()
        {
            if (Set.Count == 0 || Set == null)
            {
                return new List<string>();
            }
            
            sortSetRecursive(0, Set.Count - 1);
            
            return Set;
        }

        public MultiSet Union(MultiSet other)
        {
            if (Set.Count == 0 && other.Set.Count == 0)
            {
                return null;
            }

            MultiSet result = new MultiSet();

            result.Set.AddRange(Set);

            for (int i = 0; i < other.Set.Count; i++)
            {
                if (result.Set.Contains(other.Set[i]) && result.GetMultiplicity(other.Set[i]) >= other.GetMultiplicity(other.Set[i]))
                {
                    continue;
                }
                else
                {
                    result.Set.Add(other.Set[i]);
                }
            }

            this.ToList();
            other.ToList();
            result.ToList();

            return result;
        }

        public MultiSet Intersect(MultiSet other)
        {
            if (Set.Count == 0 && other.Set.Count == 0)
            {
                return null;
            }

            MultiSet result = new MultiSet();

            for (int i = 0; i < Set.Count; i++)
            {
                if (other.Set.Contains(Set[i]))
                {
                    if (!result.Set.Contains(Set[i]))
                    {
                        result.Set.Add(Set[i]);
                        continue;
                    }

                    uint thisMultiplicitCount = GetMultiplicity(Set[i]);
                    uint otherMultiplicitiCount = other.GetMultiplicity(Set[i]);

                    uint lessMultiplicity = thisMultiplicitCount < otherMultiplicitiCount ? thisMultiplicitCount : otherMultiplicitiCount;

                    if (lessMultiplicity > result.GetMultiplicity(Set[i]))
                    {
                        result.Set.Add(Set[i]);
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
            if (Set.Count == 0 && other.Set.Count == 0)
            {
                return null;
            }

            MultiSet result = new MultiSet();

            result.Set.AddRange(Set);
            
            for (int i = 0; i < other.Set.Count; i++)
            {
                for (int j = 0; j < result.Set.Count; j++)
                {
                    if (other.Set[i] == result.Set[j])
                    {
                        result.Set.RemoveAt(j);
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

            for (int i = 0; i < Set.Count; ++i)
            {
                string temp = Set[i];

                for (int j = 0; j < powerSets.Count; ++j)
                {
                    MultiSet addedSet = new MultiSet();
                    addedSet.Set.AddRange(powerSets[j].Set);
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
            if (Set.Count == 0)
            {
                return true;
            }
            
            MultiSet intersect = new MultiSet();
            intersect.Set.AddRange(Set);
            intersect = intersect.Intersect(other);

            if (isSameMultiSet(intersect, this))
            {
                return true;
            }

            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            if (other.Set.Count == 0 || other == null)
            {
                return true;
            }

            MultiSet intersect = new MultiSet();
            intersect.Set.AddRange(Set);
            intersect = intersect.Intersect(other);

            if (isSameMultiSet(intersect, other))
            {
                return true;
            }

            return false;
        }
        private void getConcatString()
        {
            ConcatString = string.Join("", Set);
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
            string pivot = Set[right];

            for (int i = left; i < right; ++i)
            {
                byte[] bytesLeft = Encoding.UTF8.GetBytes(Set[i]);
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

                    else
                    {
                        break;
                    }
                }
            }

            swapSetContents(left, right);

            return left;

        }
        private void swapSetContents(int i, int j)
        {
            string temp = Set[i];
            Set[i] = Set[j];
            Set[j] = temp;
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
            if (set1.Set == null && set2.Set == null)
            {
                return true;
            }

            if (set1.Set.Count != set2.Set.Count)
            {
                return false;
            }
            
            set1.ToList();
            set2.ToList();
            
            for (int i = 0; i < set1.Set.Count; i++)
            {
                if (set1.Set[i] != set2.Set[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
