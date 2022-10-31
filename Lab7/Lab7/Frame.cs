using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class Frame
    {
        public EFeatureFlags Features { get; private set; }
        public uint ID { get; private set; }
        public string Name { get; private set; }


        public Frame(uint id, string name)
        {
            ID = id;
            Name = name;
            Features = 0;
        }
        public void ToggleFeatures(EFeatureFlags features)
        {
            Features ^= features;
        }

        public void TurnOnFeatures(EFeatureFlags features)
        {
            Features |= features; 
        }

        public void TurnOffFeatures(EFeatureFlags features)
        {
            Features &= ~features;
        }
    }
}
