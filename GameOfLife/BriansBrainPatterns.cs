/*
 * Author: Tony Bierman
 * Website: http://www.tonybierman.com
 * License: MIT License
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
 * IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Abm
{
    public class BriansBrainPatterns
    {
        // Diagonal
        private static readonly (int, int)[] Diagonal = new (int, int)[]
        {
            (0, 0),
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4)
        };

        // Cross
        private static readonly (int, int)[] Cross = new (int, int)[]
        {
            (2, 0),
            (2, 1),
            (2, 2),
            (2, 3),
            (2, 4),
            (0, 2),
            (1, 2),
            (3, 2),
            (4, 2)
        };

        // Small Burst
        private static readonly (int, int)[] SmallBurst = new (int, int)[]
        {
            (1, 0),
            (2, 0),
            (3, 0),
            (0, 1),
            (4, 1),
            (0, 2),
            (4, 2),
            (0, 3),
            (4, 3),
            (1, 4),
            (2, 4),
            (3, 4)
        };

        // A simple oscillator
        public static readonly (int, int)[] Oscillator = new (int, int)[]
        {
            (1, 0),
            (1, 1),
            (1, 2)
        };

        // A spaceship-like pattern
        public static readonly (int, int)[] Spaceship = new (int, int)[]
        {
            (1, 0),
            (2, 1),
            (0, 2),
            (1, 2),
            (2, 2)
        };

        // A puffer-like pattern
        public static readonly (int, int)[] Puffer = new (int, int)[]
        {
            (1, 1),
            (2, 2),
            (0, 2),
            (1, 3),
            (2, 3),
            (3, 3),
            (4, 3)
        };

        // A simple gun-like pattern (just a representative example)
        public static readonly (int, int)[] Gun = new (int, int)[]
        {
            (1, 1),
            (1, 2),
            (2, 1),
            (2, 2),
            (2, 3),
            (3, 1),
            (4, 0),
            (5, 0)
        };

        public static (int, int)[] GetGunCoords(int startX, int startY)
        {
            return AdjustCoordinates(Gun, startX, startY);
        }

        public static (int, int)[] GetPufferCoords(int startX, int startY)
        {
            return AdjustCoordinates(Puffer, startX, startY);
        }

        public static (int, int)[] GetSpaceshipCoords(int startX, int startY)
        {
            return AdjustCoordinates(Spaceship, startX, startY);
        }

        public static (int, int)[] GetOscillatorCoords(int startX, int startY)
        {
            return AdjustCoordinates(Oscillator, startX, startY);
        }

        public static (int, int)[] GetDiagonalCoords(int startX, int startY)
        {
            return AdjustCoordinates(Diagonal, startX, startY);
        }

        public static (int, int)[] GetCrossCoords(int startX, int startY)
        {
            return AdjustCoordinates(Cross, startX, startY);
        }

        public static (int, int)[] GetSmallBurstCoords(int startX, int startY)
        {
            return AdjustCoordinates(SmallBurst, startX, startY);
        }

        private static (int, int)[] AdjustCoordinates((int, int)[] pattern, int startX, int startY)
        {
            return pattern.Select(coord => (coord.Item1 + startX, coord.Item2 + startY)).ToArray();
        }
    }
}
