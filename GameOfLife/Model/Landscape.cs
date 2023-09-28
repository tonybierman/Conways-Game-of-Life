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
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Bierman.Abm.Infrastructure;

namespace Bierman.Abm.Model
{
    public class Landscape : PropertyChangedBase
    {
        public const double CellSize = 16;
        public static Landscape DesignInstance { get; } = new();

        public int Height { get; }
        public int Width { get; }

        public (int, int)[] InitialPatternCoords { get; private set; }

        public TileManager TileManager { get; private set; }
        public AgentManager AgentManager { get; private set; }

        public IReadOnlyCollection<GameObject> GameObjects =>
            AgentManager.Agents.Cast<GameObject>().Concat(TileManager.Tiles.Cast<GameObject>()).ToList();

        public Landscape() : this(new List<AgentRule>(), null)
        {
        }

        public Landscape(List<AgentRule> rules, (int, int)[] coords) : this(40, 30, rules, coords)
        {
        }

        public Landscape(int width, int height, List<AgentRule> rules, (int, int)[] coords)
        {
            Width = width;
            Height = height;
            TileManager = new TileManager(this, width, height);
            AgentManager = new AgentManager(this, rules);

            InitializePattern(coords);
            InitialPatternCoords = coords;
        }

        private void InitializePattern((int, int)[] coords)
        {
            AgentManager.ActivateAgentAtCoordinates(coords);
        }

        public void Restart((int, int)[] coords)
        {
            AgentManager.ResetAgents();
            InitializePattern(coords);
        }

        public List<CellLocation> GetNeighbors(CellLocation cellLocation)
        {
            int[] offsets = { -1, 0, 1 };
            List<CellLocation> neighbors = new List<CellLocation>(8); // preallocate size, 3x3 minus center

            foreach (int xOffset in offsets)
            {
                foreach (int yOffset in offsets)
                {
                    // Skip the cell itself
                    if (xOffset == 0 && yOffset == 0)
                        continue;

                    int newX = cellLocation.X + xOffset;
                    int newY = cellLocation.Y + yOffset;

                    // Ensure the new location is within the game field boundaries
                    if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
                    {
                        neighbors.Add(new CellLocation(newX, newY));
                    }
                }
            }

            return neighbors;
        }
    }
}
