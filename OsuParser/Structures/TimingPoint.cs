namespace OsuParser.Structures
{
    public class TimingPoint
    {
        public TimingPoint()
        {
            Offset = 0;
            MsPerBeat = 0;
            Meter = 4;
            SampleSet = 1;
            SampleIndex = 0;
            Volume = 30;
            Inherited = false;
            Kiai = false;
        }

        public TimingPoint(double offset, double msperbeat, int meter, int sampleset, int sampleindex, int volume,
            bool inherited, bool kiai)
        {
            Offset = offset;
            MsPerBeat = msperbeat;
            Meter = meter;
            SampleSet = sampleset;
            SampleIndex = sampleindex;
            Volume = volume;
            Inherited = inherited;
            Kiai = kiai;
        }

        public TimingPoint(TimingPoint prevTimingPoint)
        {
            Offset = prevTimingPoint.Offset;
            MsPerBeat = prevTimingPoint.MsPerBeat;
            Meter = prevTimingPoint.Meter;
            SampleSet = prevTimingPoint.SampleSet;
            SampleIndex = prevTimingPoint.SampleIndex;
            Volume = prevTimingPoint.Volume;
            Inherited = prevTimingPoint.Inherited;
            Kiai = prevTimingPoint.Kiai;
        }

        public override string ToString()
        {
            return $"{Offset},{MsPerBeat},{Meter},{SampleSet},{SampleIndex},{Volume},{(Inherited ? 1 : 0)},{(Kiai ? 1 : 0)}";
        }

        public double Offset { get; set; }

        public double MsPerBeat { get; set; }

        public int Meter { get; set; }

        public int SampleSet { get; set; }

        public int SampleIndex { get; set; }

        public int Volume { get; set; }

        public bool Kiai { get; set; }

        public bool Inherited { get; set; }
    }
}