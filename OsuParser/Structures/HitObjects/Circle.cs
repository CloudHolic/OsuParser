using System;

namespace OsuParser.Structures.HitObjects
{
    class Circle : HitObject
    {
        public Circle()
        {
            Type = 1;
        }

        public Circle(int x, int y, int time, int type, int hitsound, Tuple<int, int, int, int, string> addition)
            : base(x, y, time, hitsound, addition)
        {
            Type = type;
        }

        public Circle(Circle prevCircle) : base(prevCircle)
        {
            Type = prevCircle.Type;
        }
    }
}
