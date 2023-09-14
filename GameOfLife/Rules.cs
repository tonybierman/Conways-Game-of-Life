using Bierman.Abm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Rules
    {
        public static List<AgentRule> GameOfLifeDefault()
        {
            var rules = new List<AgentRule>
            {
                agent =>
                {
                    int livingNeighborsCount = agent.Neighbors.Count(n => n.IsAlive);

                    if(agent.IsAlive)
                    {
                        if (livingNeighborsCount < 2)
                        {
                            return CellState.Dead;
                        }
                        if (livingNeighborsCount == 2 || livingNeighborsCount == 3)
                        {
                            return CellState.Alive;
                        }
                        return CellState.Dead;
                    }
                    else
                    {
                        if (livingNeighborsCount == 3)
                        {
                            return CellState.Alive;
                        }
                        return CellState.Dead;
                    }
                }
            };

            return rules;
        }

        public static List<AgentRule> BriansBrainRuleset()
        {
            var rules = new List<AgentRule>
            {
                agent =>
                {
                    if(agent.CurrentState == CellState.Alive)
                    {
                        return CellState.Dying;
                    }
                    else if(agent.CurrentState == CellState.Dying)
                    {
                        return CellState.Dead;
                    }
                    else // Current state is Dead
                    {
                        int livingNeighborsCount = agent.Neighbors.Count(n => n.CurrentState == CellState.Alive);
                        if (livingNeighborsCount == 2)
                        {
                            return CellState.Alive;
                        }
                        return CellState.Dead;
                    }
                }
            };

            return rules;
        }
    }
}
