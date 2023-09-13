using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia.Input;

namespace Bierman.Abm.Model;

public class Game : GameBase
{
    private readonly Landscape _field;

    public Game(Landscape field)
    {
        _field = field;
    }

    protected override void Tick()
    {
        // Every second
        if (CurrentTick % 20 == 0)
        {
            foreach (var ag in _field.GameObjects.OfType<Agent>())
            {
                ag.FutureState = ag.NextState();
            }

            foreach (var ag in _field.GameObjects.OfType<Agent>())
            {
                ag.IsAlive = ag.FutureState == CellState.Alive;
            }
        }
    }
}