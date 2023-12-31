﻿using Bierman.Abm.Model;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Abm.Model
{
    public class AgentManager
    {
        private readonly Landscape _landscape;
        public ObservableCollection<Agent> Agents { get; } = new ObservableCollection<Agent>();

        public AgentManager(Landscape landscape, List<AgentRule> rules)
        {
            _landscape = landscape;
            InitializeAgents(rules);
        }

        private void InitializeAgents(List<AgentRule> rules)
        {
            for (var x = 0; x < _landscape.Width; x++)
            {
                for (var y = 0; y < _landscape.Height; y++)
                {
                    var agent = new Agent(_landscape, new CellLocation(x, y), rules);
                    Agents.Add(agent);
                }
            }
        }

        public void ActivateAgentAtCoordinates((int, int)[] coords)
        {
            foreach (var coord in coords)
            {
                var agentToActivate = Agents.FirstOrDefault(t => CellLocation.FromPoint(t.Location).X == coord.Item1 && CellLocation.FromPoint(t.Location).Y == coord.Item2);
                if (agentToActivate != null)
                    agentToActivate.CurrentState = CellState.Alive;
            }
        }

        public void ResetAgents()
        {
            foreach (var agent in Agents)
            {
                agent.CurrentState = CellState.Dead;
            }
        }

        public List<Agent> GetNeighborAgentsForAgent(Agent targetAgent)
        {
            var neighborLocations = _landscape.GetNeighbors(CellLocation.FromPoint(targetAgent.Location));
            var neighborSet = new HashSet<CellLocation>(neighborLocations);

            return Agents
                .Where(agent => neighborSet.Contains(CellLocation.FromPoint(agent.Location)))
                .ToList();
        }
    }
}
