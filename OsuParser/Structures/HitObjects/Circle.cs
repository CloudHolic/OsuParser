using System;

namespace OsuParser.Structures.HitObjects
{
    class Circle : HitObject
    {
        public Circle()
        {
            Type = 1;
        }

        public Circle(int x, int y, int time, int type, int hitsound, Tuple<int, int, int, int, string> extras)
            : base(x, y, time, hitsound, extras)
        {
            Type = type;
        }

        public Circle(Circle prevCircle) : base(prevCircle)
        {
            Type = prevCircle.Type;
        }

        public override string ToString()
        {
            return $"{X},{Y},{Time},{Type},{HitSound},{ExtraToString()}";
        }
    }
}
