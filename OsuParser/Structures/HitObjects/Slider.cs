using System;
using System.Collections.Generic;

namespace OsuParser.Structures.HitObjects
{
    class Slider : HitObject
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
            IEnumerable<Tuple<int, int>> edgeAdditions, Tuple<int, int, int, int, string> addition)
            : base(x, y, time, hitsound, addition)
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

        public char SliderType { get; set; }

        public List<Tuple<int, int>> CurvePoints { get; set; }

        public int Repeat { get; set; }

        public float PixelLength { get; set; }

        public List<int> EdgeHitsounds { get; set; }

        public List<Tuple<int, int>> EdgeAdditions { get; set; }
    }
}
