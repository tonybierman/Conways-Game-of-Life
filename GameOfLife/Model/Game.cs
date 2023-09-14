using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia.Input;

namespace Bierman.Abm.Model
{
    public class Game : GameBase
    {
        private readonly Landscape _field;
        private List<Agent> _cachedAgents;

        public Game(Landscape field)
        {
            _field = field;
        }

        protected override void Tick()
        {
            // Every 3 ticks
            if (CurrentTick % 3 == 0)
            {
                // Caching the list of agents (if they aren't changing frequently)
                if (_cachedAgents == null)
                {
                    _cachedAgents = _field.GameObjects.OfType<Agent>().ToList();
                }

                // Store updates to be batch-applied
                var updates = new Dictionary<Agent, CellState>();

                foreach (var agent in _cachedAgents)
                {
                    var futureState = agent.NextState();
                    updates[agent] = futureState.Value;
                }

                // Apply updates
                foreach (var update in updates)
                {
                    update.Key.CurrentState = update.Value;
                }
            }
        }
    }
}
