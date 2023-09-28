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
using Bierman.Abm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Abm
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
