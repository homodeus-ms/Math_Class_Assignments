using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Recyclebot
    {
        public List<Item> RecycleItems { get; private set; } = new List<Item>();
        public List<Item> NonRecycleItems { get; private set; } = new List<Item>();

        private List<Item> mDumpItems;
        

        public void Add(Item item)
        {
            if ((item.Type == EType.Paper || item.Type == EType.Furniture || item.Type == EType.Electronics) && 
                (item.Weight >= 5.0 || item.Weight < 2.0)) 
            {
                NonRecycleItems.Add(item);
            }
            else
            {
                RecycleItems.Add(item);
            }
        }

        public List<Item> Dump()
        {
            mDumpItems = new List<Item>();

            foreach (Item item in NonRecycleItems)
            {
                if (item.Type == EType.Furniture || item.Type == EType.Electronics)
                {
                    mDumpItems.Add(item);
                }
            }

            return mDumpItems;
        }
    }
}
