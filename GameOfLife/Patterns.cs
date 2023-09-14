using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Abm
{
    public class Patterns
    {
        // Glider
        private static readonly (int, int)[] Glider = new (int, int)[]
        {
            (1, 0),
            (2, 1),
            (0, 2),
            (1, 2),
            (2, 2)
        };

        // Lightweight Spaceship (LWSS)
        private static readonly (int, int)[] LWSS = new (int, int)[]
        {
            (5, 0),
            (9, 0),
            (4, 1),
            (9, 1),
            (9, 2),
            (4, 3),
            (5, 3),
            (6, 3),
            (7, 3),
            (8, 3)
        };

        // Block (Still Life)
        private static readonly (int, int)[] Block = new (int, int)[]
        {
            (1, 1),
            (1, 2),
            (2, 1),
            (2, 2)
        };

        // Blinker (Oscillator)
        private static readonly (int, int)[] Blinker = new (int, int)[]
        {
            (1, 0),
            (1, 1),
            (1, 2)
        };

        // Gosper Glider Gun (example for Glider Gun)
        private static readonly (int, int)[] GliderGun = new (int, int)[]
        {
            (1, 5), (1, 6), (2, 5), (2, 6),
            (11, 5), (11, 6), (11, 7), (12, 4), (12, 8),
            (13, 3), (13, 9), (14, 3), (14, 9), (15, 6),
            (16, 4), (16, 8), (17, 5), (17, 6), (17, 7), (18, 6),
            (21, 3), (21, 4), (21, 5), (22, 3), (22, 4), (22, 5),
            (23, 2), (23, 6), (25, 1), (25, 2), (25, 6), (25, 7)
        };

        public static (int, int)[] GetGliderCoords(int startX, int startY)
        {
            return AdjustCoordinates(Glider, startX, startY);
        }

        public static (int, int)[] GetLWSSCoords(int startX, int startY)
        {
            return AdjustCoordinates(LWSS, startX, startY);
        }

        public static (int, int)[] GetBlockCoords(int startX, int startY)
        {
            return AdjustCoordinates(Block, startX, startY);
        }

        public static (int, int)[] GetBlinkerCoords(int startX, int startY)
        {
            return AdjustCoordinates(Blinker, startX, startY);
        }

        public static (int, int)[] GetGliderGunCoords(int startX, int startY)
        {
            return AdjustCoordinates(GliderGun, startX, startY);
        }

        private static (int, int)[] AdjustCoordinates((int, int)[] pattern, int startX, int startY)
        {
            return pattern.Select(coord => (coord.Item1 + startX, coord.Item2 + startY)).ToArray();
        }
    }
}
