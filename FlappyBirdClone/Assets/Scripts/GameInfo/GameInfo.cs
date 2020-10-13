using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo
{
    private static GameMode currentGameMode;

    internal static void SetGameMode(GameMode _currentGameMode)
    {
        currentGameMode = _currentGameMode;
    }

    internal static GameMode GetGameMode()
    {
        return currentGameMode;
    }
}
