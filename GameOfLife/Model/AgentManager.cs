using Bierman.Abm.Model;
using System;
using System.Collections.Generic;
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
