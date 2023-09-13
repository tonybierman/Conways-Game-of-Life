﻿using System.Collections.Generic;
using Avalonia.Input;

namespace Bierman.Abm;

internal static class Keyboard
{
    public static readonly HashSet<Key> Keys = new();

    public static bool IsKeyDown(Key key)
    {
        return Keys.Contains(key);
    }
}