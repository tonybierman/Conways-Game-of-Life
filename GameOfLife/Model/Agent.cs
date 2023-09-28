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

        public virtual CellState? NextState()
        {
            if (Neighbors == null)
                Neighbors = _field.AgentManager.GetNeighborAgentsForAgent(this).ToList();

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
