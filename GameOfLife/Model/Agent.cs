using Avalonia.Controls.Embedding.Offscreen;
using System.Collections.Generic;
using System.Linq;

namespace Bierman.Abm.Model;

public class Agent : MovingGameObject
{
    private bool _isAlive = false;

    public List<Agent> Neighbors { get; private set; }

    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if (value.Equals(_isAlive)) return;
            _isAlive = value;
            OnPropertyChanged(nameof(IsAlive));
        }
    }

    public CellState FutureState { get; set; }

    public Agent(Landscape field, CellLocation location) : base(field, location)
    { 

    }

    public CellState NextState()
    {
        if(Neighbors == null)
            Neighbors = _field.GetNeighborGameObjectsForGameObject(this).OfType<Agent>().ToList();

        int livingNeighborsCount = Neighbors.Where(n => n.IsAlive == true).Count();

        if (IsAlive == true)
        {
            if (livingNeighborsCount < 2)
            {
                return CellState.Dead;
            }
            else if (livingNeighborsCount == 2 || livingNeighborsCount == 3)
            {
                return CellState.Alive;
            }
            else
            {
                return CellState.Dead;
            }
        }
        else
        {
            if (livingNeighborsCount == 3)
            {
                return CellState.Alive;
            }
            else
            {
                return CellState.Dead;
            }
        }
    }
}