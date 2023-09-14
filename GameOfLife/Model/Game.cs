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
            if (CurrentTick % 3 == 0)
            {
                // Caching the list of agents (if they aren't changing frequently)
                if (_cachedAgents == null)
                {
                    _cachedAgents = _field.GameObjects.OfType<Agent>().ToList();
                }

                // First, determine the future state for every agent without applying it yet
                foreach (var agent in _cachedAgents)
                {
                    agent.NextState();
                }

                // Now, update each agent's current state with its determined future state
                foreach (var agent in _cachedAgents)
                {
                    agent.CurrentState = agent.FutureState;
                }
            }
        }
    }
}
