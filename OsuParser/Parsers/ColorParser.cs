﻿using OsuParser.Exceptions;
using OsuParser.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace OsuParser.Parsers
{
    public static class ColorParser
    {
        public static List<Colors> Parse(string filename)
        {
            var data = new List<Colors>();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //  Find Colours Section.
                    if (currentLine == "[Colours]")
                    {
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (!currentLine.StartsWith("Combo"))
                                break;

                            var parsed = currentLine.Substring(currentLine.IndexOf(':') + 1).Split(',');
                            if (parsed.Length != 3)
                                throw new InvalidBeatmapException("Wrong Colour Found.");

                            var temp = new Colors
                            {
                                //  Number
                                Number = Convert.ToInt32(currentLine.Substring(5).Split(':')[0].Trim()),

                                //  Red
                                Red = Convert.ToInt32(parsed[0]),

                                //  Green
                                Green = Convert.ToInt32(parsed[1]),

                                //  Blue
                                Blue = Convert.ToInt32(parsed[2])
                            };

                            data.Add(temp);
                        }
                    }
                }
            }

            if (data.Count == 0)
                data.Add(new Colors());

            return data;
        }

        internal static void Writer(StreamWriter writer, List<Colors> colors)
        {
            // If colors' first element's number == 0, then skip
            if (colors.Count == 1 && colors[0].Number == 0)
                return;

            // Section Header
            writer.WriteLine();
            writer.WriteLine("[Colours]");

            // Each Combo Colors
            foreach (var color in colors)
                writer.WriteLine(color.ToString());
        }
    }
}