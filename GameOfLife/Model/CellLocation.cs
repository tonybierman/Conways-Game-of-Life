using Avalonia;
using System;

namespace Bierman.Abm.Model
{
    public readonly record struct CellLocation(int X, int Y)
    {
        public Point ToPoint()
        {
            return new(Landscape.CellSize * X, Landscape.CellSize * Y);
        }

        public static CellLocation FromPoint(Point point)
        {
            int x = (int)Math.Round(point.X / Landscape.CellSize);
            int y = (int)Math.Round(point.Y / Landscape.CellSize);

            return new CellLocation(x, y);
        }
    }
}
