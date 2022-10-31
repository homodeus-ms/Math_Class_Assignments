using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab7
{
    public static class FilterEngine
    {
        public static List<Frame> FilterFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> result = new List<Frame>(frames.Count);

            for (int i = 0; i < frames.Count; ++i)
            {
                if ((int)frames[i].Features != (int)(frames[i].Features & ~features))
                {
                    result.Add(frames[i]);
                }
            }
            
            return result;
        }
        public static List<Frame> FilterOutFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> result = new List<Frame>(frames.Count);

            for (int i = 0; i < frames.Count; ++i)
            {
                if ((int)frames[i].Features == (int)(frames[i].Features & ~features))
                {
                    result.Add(frames[i]);
                }
            }

            return result;
        }
        public static List<Frame> Intersect(List<Frame> frame1, List<Frame> frame2)
        {
            List<Frame> interSection = new List<Frame>(frame1.Count + frame2.Count);

            frame2 = frame2.OrderBy(x => x.ID).ToList();

            for (int i = 0; i < frame1.Count; ++i)
            {
                if (FindValueRecursive(frame2, frame1[i].ID, 0, frame2.Count - 1))
                {
                    interSection.Add(frame1[i]);
                }
            }

            return interSection;
        }
        public static List<int> GetSortKeys(List<Frame> frames, List<EFeatureFlags> features)
        {
            List<int> sortKeys = new List<int>(frames.Count);
            
            for (int i = 0; i < sortKeys.Capacity; ++i)
            {
                sortKeys.Add(0);
            }

            int count = 1 << features.Count;

            for (int i = 0; i < features.Count; ++i)
            {
                for (int j = 0; j < frames.Count; ++j)
                {
                    if ((int)frames[j].Features != (int)(frames[j].Features & ~features[i]))
                    {
                        sortKeys[j] += count;
                    }
                }

                count >>= 1;
            }

            return sortKeys;
        }

        private static bool FindValueRecursive(List<Frame> frame, uint ID, int start, int end)
        { 
            int mid = (start + end) / 2;

            if (start > end)
            {
                return false;
            }

            if (ID == frame[mid].ID)
            {
                return true;
            }

            else if (ID < frame[mid].ID)
            {
                return FindValueRecursive(frame, ID, start, mid - 1);
            }
            
            else
            {
                return FindValueRecursive(frame, ID, mid + 1, end);
            }
        }
    }
}
