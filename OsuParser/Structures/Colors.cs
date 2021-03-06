﻿namespace OsuParser.Structures
{
    public class Colors
    {
        public Colors()
        {
            Number = 0;

            Red = 0;
            Green = 0;
            Blue = 0;
        }

        public Colors(int num, int r, int g, int b)
        {
            Number = num;

            Red = r;
            Green = g;
            Blue = b;
        }

        public Colors(Colors prevColours)
        {
            Number = prevColours.Number;

            Red = prevColours.Red;
            Green = prevColours.Green;
            Blue = prevColours.Blue;
        }

        public override string ToString()
        {
            return $"Combo{Number} : {Red},{Green},{Blue}";
        }

        public int Number { get; set; }

        public int Red { get; set; }

        public int Green { get; set; }

        public int Blue { get; set; }
    }
}