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
                    _cachedAgents = _field.AgentManager.Agents.ToList();
                }

                // Check if all agents are dead
                if (_cachedAgents.All(agent => agent.CurrentState == CellState.Dead))
                {
                    _field.Restart(_field.InitialPatternCoords);
                    return;  // Exit the Tick method to prevent further processing for this tick
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
