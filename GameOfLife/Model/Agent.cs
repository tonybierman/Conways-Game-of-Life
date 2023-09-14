using Avalonia.Controls.Embedding.Offscreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bierman.Abm.Model
{
    public class Agent : MovingGameObject
    {
        private bool _isAlive = false;
        private readonly List<AgentRule> _rules;

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

        public Agent(Landscape field, CellLocation location, List<AgentRule> rules)
        : base(field, location)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public CellState? NextState()
        {
            if (Neighbors == null)
                Neighbors = _field.GetNeighborGameObjectsForGameObject(this).OfType<Agent>().ToList();

            foreach (var rule in _rules)
            {
                var result = rule(this);
                if (result.HasValue)
                    return result;
            }

            return CellState.Dead; // Default if no rules apply
        }
    }
}
