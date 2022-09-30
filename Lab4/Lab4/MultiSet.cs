using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    public sealed class MultiSet
    {
        public List<string> Elements { get; private set; } = new List<string>();
        public string ElementsConcat { get; private set; }

        public void Add(string element)
        {
            Elements.Add(element);
        }

        public bool Remove(string element)
        {
            return Elements.Remove(element);
        }

        public uint GetMultiplicity(string element)
        {
            uint count = 0;

            for (int i = 0; i < Elements.Count; ++i)
            {
                if (Elements[i] == element)
                {
                    count++;
                }
            }
            
            return count;
        }

        public List<string> ToList()
        {
            if (Elements.Count == 0 || Elements == null)
            {
                return new List<string>();
            }

            List<string> result = Support.OrderList(Elements);
            
            return result;
        }

        public MultiSet Union(MultiSet other)
        {
            if (other.Elements.Count == 0)
            {
                return this;
            }
            if (Elements.Count == 0)
            {
                return other;
            }

            MultiSet result = new MultiSet();

            result.Elements.AddRange(this.Elements);

            for (int i = 0; i < other.Elements.Count; ++i)
            {
                if (!result.Elements.Contains(other.Elements[i]))
                {
                    result.Add(other.Elements[i]);
                }

                else if (result.GetMultiplicity(other.Elements[i]) < other.GetMultiplicity(other.Elements[i]))
                {
                    result.Add(other.Elements[i]);
                }
            }
            
            return result;
        }

        public MultiSet Intersect(MultiSet other)
        {
            if (other.Elements.Count == 0 || this.Elements.Count == 0)
            {
                return new MultiSet();
            }

            MultiSet result = new MultiSet();

            MultiSet shorterSet = Elements.Count < other.Elements.Count ? this : other;
            MultiSet longerSet = Elements.Count < other.Elements.Count ? other : this;

            for (int i = 0; i < shorterSet.Elements.Count; ++i)
            {
                uint shorterSetMultiplicity = shorterSet.GetMultiplicity(shorterSet.Elements[i]);
                uint longerSetMultiplicity = longerSet.GetMultiplicity(shorterSet.Elements[i]);

                if (shorterSetMultiplicity == longerSetMultiplicity)
                {
                    result.Add(shorterSet.Elements[i]);
                    continue;
                }

                uint lessMultiplicity = shorterSetMultiplicity < longerSetMultiplicity ? shorterSetMultiplicity : longerSetMultiplicity;

                if (result.GetMultiplicity(shorterSet.Elements[i]) < lessMultiplicity)
                {
                    result.Add(shorterSet.Elements[i]);
                }
            }

            return result;
        }

        public MultiSet Subtract(MultiSet other)
        {
            if (other.Elements.Count == 0)
            {
                return this;
            }

            if (this.Elements.Count == 0)
            {
                return new MultiSet();
            }

            MultiSet result = new MultiSet();

            result.Elements.AddRange(Elements);

            MultiSet intersection = this.Intersect(other);

            for (int i = 0; i < intersection.Elements.Count; ++i)
            {
                result.Remove(intersection.Elements[i]);
            }

            return result;
        }

        public List<MultiSet> FindPowerSet()
        {
            List<MultiSet> result = new List<MultiSet>();

            if (Elements.Count == 0)
            {
                result.Add(new MultiSet());

                return result;
            }
            
            Support.GetPowerSetsRecursive(Elements, result, Elements.Count - 1);
            
            for (int i = 0; i < result.Count; ++i)
            {
                result[i].ToList();
                result[i].ElementsConcat = string.Join("", result[i].Elements);
            }

            Support.DeleteDuplicates(result);

            result = result.OrderBy(x => x.ElementsConcat).ToList();

            return result;
        }

        public bool IsSubsetOf(MultiSet other)
        {
            if (Elements.Count == 0)
            {
                return true;
            }

            MultiSet intersection = this.Intersect(other);
            this.ToList();
            intersection.ToList();

            if (Support.IsSameList(intersection.Elements, this.Elements))
            {
                return true;
            }

            return false;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            if (other.Elements.Count == 0)
            {
                return true;
            }

            MultiSet intersection = this.Intersect(other);

            if (Support.IsSameList(intersection.Elements, other.Elements))
            {
                return true;
            }

            return false;
        }
    }
}