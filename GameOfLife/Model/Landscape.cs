using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Bierman.Abm.Infrastructure;

namespace Bierman.Abm.Model;

public class Landscape : PropertyChangedBase
{
    public const double CellSize = 16;

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
        Tiles = new LandscapeTile[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                GameObjects.Add(
                    Tiles[x, y] =
                        new LandscapeTile(new Point(x * CellSize, y * CellSize)));

                var t = new Agent(this, new CellLocation(x, y), rules);

                GameObjects.Add(t);
            }
        }

        foreach (var coord in coords)
        {
            var agentToActivate = GameObjects.OfType<Agent>().FirstOrDefault(t => CellLocation.FromPoint(t.Location).X == coord.Item1 && CellLocation.FromPoint(t.Location).Y == coord.Item2);
            agentToActivate!.CurrentState = CellState.Alive;
        }
    }

    public static Landscape DesignInstance { get; } = new();

    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public LandscapeTile[,] Tiles { get; }

    public int Height { get; }
    public int Width { get; }

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

    public List<GameObject> GetNeighborGameObjectsForGameObject(GameObject targetGameObject)
    {
        var neighborLocations = GetNeighbors(CellLocation.FromPoint(targetGameObject.Location));
        var neighborSet = new HashSet<CellLocation>(neighborLocations);

        return GameObjects
            .Where(go => neighborSet.Contains(CellLocation.FromPoint(go.Location)))
            .ToList();
    }

}