namespace OsuParser.Structures.Events
{
    public class SbSound
    {
        public SbSound()
        {
            Time = 0;
            Layer = SbLayer.Background;
            FileName = string.Empty;
            Volume = 0;
        }

        public SbSound(int time, SbLayer layer, string fileName, int volume)
        {
            Time = time;
            Layer = layer;
            FileName = fileName;
            Volume = volume;
        }

        public SbSound(SbSound prevSound)
        {
            Time = prevSound.Time;
            Layer = prevSound.Layer;
            FileName = prevSound.FileName;
            Volume = prevSound.Volume;
        }

        public override string ToString()
        {
            return $"Sample,{Time},{Layer.ToString()},\"{FileName}\",{Volume}";
        }

        public int Time { get; set; }

        public SbLayer Layer { get; set; }

        public string FileName { get; set; }

        public int Volume { get; set; }
    }
}