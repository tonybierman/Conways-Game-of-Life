using Avalonia.Controls.Embedding.Offscreen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bierman.Abm.Model
{
    public class Agent : MovingGameObject
    {
        private readonly List<AgentRule> _rules;
        private CellState _currentState;

        public List<Agent> Neighbors { get; private set; }

        public bool IsAlive => CurrentState == CellState.Alive;

        public bool IsDying => CurrentState == CellState.Dying;

        public CellState CurrentState
        {
            get => _currentState;
            set
            {
                if (value.Equals(_currentState)) return;

                _currentState = value;
                OnPropertyChanged(nameof(CurrentState));
                OnPropertyChanged(nameof(IsAlive));
                OnPropertyChanged(nameof(IsDying));
            }
        }

        public CellState FutureState { get; set; }

        public Agent(Landscape field, CellLocation location, List<AgentRule> rules)
        : base(field, location)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            CurrentState = CellState.Dead;
        }

        public CellState? NextState()
        {
            if (Neighbors == null)
                Neighbors = _field.GetNeighborGameObjectsForGameObject(this).OfType<Agent>().ToList();

            foreach (var rule in _rules)
            {
                var result = rule(this);
                if (result.HasValue)
                {
                    FutureState = result.Value;
                    return result;
                }
            }

            FutureState = CellState.Dead; // Default if no rules apply
            return FutureState;
        }
    }
}
