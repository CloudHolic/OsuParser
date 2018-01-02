using System;
using System.Collections.Generic;

namespace OsuParser.Structures.HitObjects
{
    internal class RawHitObject
    {
        internal RawHitObject()
        {
            X = 0;
            Y = 0;
            Time = 0;
            Type = 1;
            Hitsound = 0;
            SliderType = 'L';
            CurvePoints = new List<Tuple<int, int>>();
            Repeat = 1;
            PixelLength = 0;
            EdgeHitsounds = new List<int>();
            EdgeAdditions = new List<Tuple<int, int>>();
            EndTime = 0;
            Extras = Tuple.Create(0, 0, 0, 0, string.Empty);
        }

        internal RawHitObject(int x, int y, int time, int type, int hitsound, char sliderType,
            IEnumerable<Tuple<int, int>> curvePoints, int repeat, float pixelLength, IEnumerable<int> edgeHItsounds,
            IEnumerable<Tuple<int, int>> edgeAdditions, int endTime, Tuple<int, int, int, int, string> extras)
        {
            X = x;
            Y = y;
            Time = time;
            Type = type;
            Hitsound = hitsound;
            SliderType = sliderType;
            CurvePoints = new List<Tuple<int, int>>(curvePoints);
            Repeat = repeat;
            PixelLength = pixelLength;
            EdgeHitsounds = new List<int>(edgeHItsounds);
            EdgeAdditions = new List<Tuple<int, int>>(edgeAdditions);
            EndTime = endTime;
            Extras = extras;
        }

        internal RawHitObject(RawHitObject prevRawHitObject)
        {
            X = prevRawHitObject.X;
            Y = prevRawHitObject.Y;
            Time = prevRawHitObject.Time;
            Type = prevRawHitObject.Type;
            Hitsound = prevRawHitObject.Hitsound;
            SliderType = prevRawHitObject.SliderType;
            CurvePoints = new List<Tuple<int, int>>(prevRawHitObject.CurvePoints);
            Repeat = prevRawHitObject.Repeat;
            PixelLength = prevRawHitObject.PixelLength;
            EdgeHitsounds = new List<int>(prevRawHitObject.EdgeHitsounds);
            EdgeAdditions = new List<Tuple<int, int>>(prevRawHitObject.EdgeAdditions);
            EndTime = prevRawHitObject.EndTime;
            Extras = prevRawHitObject.Extras;
        }

        internal int X { get; set; }

        internal int Y { get; set; }

        internal int Time { get; set; }

        internal int Type { get; set; }

        internal int Hitsound { get; set; }

        internal char SliderType { get; set; }

        internal List<Tuple<int, int>> CurvePoints { get; set; }

        internal int Repeat { get; set; }

        internal float PixelLength { get; set; }

        internal List<int> EdgeHitsounds { get; set; }

        internal List<Tuple<int, int>> EdgeAdditions { get; set; }

        internal int EndTime { get; set; }

        internal Tuple<int, int, int, int, string> Extras { get; set; }
    }
}