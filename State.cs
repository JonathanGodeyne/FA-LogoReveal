using System;
using System.Collections.Generic;

using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Febelfin_academy_Logo_reveal
{
    public static class State
    {
        public static void Save(List<Rectangle> rectangles)
        {
            using (StreamWriter sw = File.CreateText("Coordinates.csv"))
            {
                foreach (Rectangle rect in rectangles)
                {
                    sw.WriteLine($"{Grid.GetRow(rect)};{Grid.GetColumn(rect)};{rect.Fill}");
                }
            }
        }

        public static List<Rectangle> Load()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            var cords = File.ReadAllLines("Coordinates.csv");

            foreach (var c in cords)
            {
                string[] line = c.Split(';');
                Rectangle rect = new Rectangle();
                Grid.SetRow(rect, int.Parse(line[0]));
                Grid.SetColumn(rect, int.Parse(line[1]));
                rect.Fill = line[2] == string.Empty ? null : (SolidColorBrush)new BrushConverter().ConvertFrom(line[2]);
                rectangles.Add(rect);
            }
            return rectangles;
        }

        public static bool Exists()
        {
            if (File.Exists("Coordinates.csv"))
            {
                return true;
            }
            return false;
        }
    }
}
