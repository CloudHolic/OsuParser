using System;

namespace OsuParser.Structures.HitObjects
{
    public abstract class HitObject
    {
        protected HitObject()
        {
            X = 0;
            Y = 0;
            Time = 0;
            HitSound = 0;
            Addition = Tuple.Create(0, 0, 0, 0, string.Empty);
        }

        protected HitObject(int x, int y, int time, int hitsound, Tuple<int, int, int, int, string> addition)
        {
            X = x;
            Y = y;
            Time = time;
            HitSound = hitsound;
            Addition = addition;
        }

        protected HitObject(HitObject prevHitObject)
        {
            X = prevHitObject.X;
            Y = prevHitObject.Y;
            Time = prevHitObject.Time;
            HitSound = prevHitObject.HitSound;
            Addition = prevHitObject.Addition;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Time { get; set; }

        internal int Type { get; set; }

        public int HitSound { get; set; }

        public Tuple<int, int, int, int, string> Addition { get; set; }
    }
}
