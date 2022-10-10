using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Item
    {
        public EType Type { get; private set; }
        public double Weight { get; private set; }
        public double Volume { get; private set; }
        public bool IsToxicWaste { get; private set; }

        public Item (EType type, double weight, double volume, bool bToxicWaste)
        {
            Type = type;
            Weight = weight;
            Volume = volume;
            IsToxicWaste = bToxicWaste;
        }


    }
}
