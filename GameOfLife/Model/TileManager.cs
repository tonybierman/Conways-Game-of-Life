using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Abm.Model
{
    public class TileManager
    {
        private readonly Landscape _landscape;

        public LandscapeTile[,] Tiles { get; }

        public TileManager(Landscape landscape, int width, int height)
        {
            _landscape = landscape;
            Tiles = new LandscapeTile[width, height];
            InitializeTiles();
        }

        private void InitializeTiles()
        {
            for (var x = 0; x < _landscape.Width; x++)
            {
                for (var y = 0; y < _landscape.Height; y++)
                {
                    Tiles[x, y] = new LandscapeTile(new Point(x * Landscape.CellSize, y * Landscape.CellSize));
                }
            }
        }
    }
}
