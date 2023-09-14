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
    }
}
