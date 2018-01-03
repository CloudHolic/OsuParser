using OsuParser;

namespace OusParserTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var sampleOsb = Parser.LoadOsbFile("C:/Users/PC-011/Downloads/Suzuta Miyako - one's future (Full Ver.) (DJPop).osb");
            Parser.SaveOsbFile("C:/Users/PC-011/Downloads/Sample.osb", sampleOsb);

            var sampleOsu = Parser.LoadOsuFile("C:/Users/PC-011/Downloads/1.osu");
            Parser.SaveOsuFile("C:/Users/PC-011/Downloads/Sample.osu", sampleOsu);
        }
    }
}
