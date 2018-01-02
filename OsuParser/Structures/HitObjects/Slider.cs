using System;
using System.Collections.Generic;
using System.Linq;

namespace OsuParser.Structures.HitObjects
{
    public class Slider : HitObject
    {
        public Slider()
        {
            Type = 2;
            SliderType = 'L';
            CurvePoints = new List<Tuple<int, int>>();
            Repeat = 1;
            PixelLength = 0;
            EdgeHitsounds = new List<int>();
            EdgeAdditions = new List<Tuple<int, int>>();
        }

        public Slider(int x, int y, int time, int type, int hitsound, char sliderType,
            IEnumerable<Tuple<int, int>> curvePoints, int repeat, float pixelLength, IEnumerable<int> edgeHItsounds,
            IEnumerable<Tuple<int, int>> edgeAdditions, Tuple<int, int, int, int, string> extras)
            : base(x, y, time, hitsound, extras)
        {
            Type = type;
            SliderType = sliderType;
            CurvePoints = new List<Tuple<int, int>>(curvePoints);
            Repeat = repeat;
            PixelLength = pixelLength;
            EdgeHitsounds = new List<int>(edgeHItsounds);
            EdgeAdditions = new List<Tuple<int, int>>(edgeAdditions);
        }

        public Slider(Slider prevSlider) : base(prevSlider)
        {
            Type = prevSlider.Type;
            SliderType = prevSlider.SliderType;
            CurvePoints = new List<Tuple<int, int>>(prevSlider.CurvePoints);
            Repeat = prevSlider.Repeat;
            PixelLength = prevSlider.PixelLength;
            EdgeHitsounds = new List<int>(prevSlider.EdgeHitsounds);
            EdgeAdditions = new List<Tuple<int, int>>(prevSlider.EdgeAdditions);
        }

        private string TupleListToString(List<Tuple<int, int>> tupleList)
        {
            return string.Join("|", tupleList.Select(cur => $"{cur.Item1}:{cur.Item2}"));
        }

        public override string ToString()
        {
            return $"{X},{Y},{Time},{Type},{HitSound},{SliderType}|{TupleListToString(CurvePoints)},{Repeat},{PixelLength}," +
                   $"{string.Join("|", EdgeHitsounds)},{TupleListToString(EdgeAdditions)},{ExtraToString()}";
        }

        public char SliderType { get; set; }

        public List<Tuple<int, int>> CurvePoints { get; set; }

        public int Repeat { get; set; }

        public float PixelLength { get; set; }

        public List<int> EdgeHitsounds { get; set; }

        public List<Tuple<int, int>> EdgeAdditions { get; set; }
    }
}
