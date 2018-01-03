using System.Collections.Generic;

namespace OsuParser.Structures
{
    public class Editor
    {
        internal Editor()
        {
            Bookmarks = new List<int>();
            DistanceSpacing = 0;
            BeatDivisor = 4;
            GridSize = 1;
            TimelineZoom = 1;
        }

        internal Editor(List<int> bookmarks, double distance, int beat, int grid, double timeline)
        {
            Bookmarks = bookmarks;
            DistanceSpacing = distance;
            BeatDivisor = beat;
            GridSize = grid;
            TimelineZoom = timeline;
        }

        internal Editor(Editor prevEditor)
        {
            Bookmarks = new List<int>(prevEditor.Bookmarks);
            DistanceSpacing = prevEditor.DistanceSpacing;
            BeatDivisor = prevEditor.BeatDivisor;
            GridSize = prevEditor.GridSize;
            TimelineZoom = prevEditor.TimelineZoom;
        }

        public List<int> Bookmarks { get; set; }

        public double DistanceSpacing { get; set; }

        public int BeatDivisor { get; set; }

        public int GridSize { get; set; }

        public double TimelineZoom { get; set; }
    }
}