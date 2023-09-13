using Avalonia.Controls.Embedding.Offscreen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bierman.Abm.Model
{
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

        // Define our list of rules
        // Based on Conway's Game of Life
        private List<Func<bool, int, CellState?>> rules = new List<Func<bool, int, CellState?>>
        {
            (isAlive, count) => isAlive && count < 2 ? CellState.Dead : (CellState?)null,
            (isAlive, count) => isAlive && (count == 2 || count == 3) ? CellState.Alive : (CellState?)null,
            (isAlive, count) => isAlive ? CellState.Dead : (CellState?)null,
            (isAlive, count) => !isAlive && count == 3 ? CellState.Alive : (CellState?)null,
            (isAlive, count) => !isAlive ? CellState.Dead : (CellState?)null
        };

        public CellState NextState()
        {
            if (Neighbors == null)
                Neighbors = _field.GetNeighborGameObjectsForGameObject(this).OfType<Agent>().ToList();

            int livingNeighborsCount = Neighbors.Where(n => n.IsAlive).Count();

            // Check each rule in order and return the first matching result
            foreach (var rule in rules)
            {
                CellState? result = rule(IsAlive, livingNeighborsCount);
                if (result.HasValue)
                    return result.Value;
            }

            // Default return if none of the rules match (shouldn't get here based on our rules)
            return CellState.Dead;
        }
    }
}
