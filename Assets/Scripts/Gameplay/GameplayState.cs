using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayState
{
    protected GameplayManager gameplayManager;
    public GameplayState(GameplayManager manager)
    {
        gameplayManager = manager;
    }
    public abstract void Apply();
}
