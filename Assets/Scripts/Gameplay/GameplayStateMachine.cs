using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateMachine
{
    private GameplayState currentState;

    public GameplayStateMachine(GameplayState initialState)
    {
        currentState = initialState;
    }

    public void SetState(GameplayState state)
    {
        this.currentState = state;
    }

    public void Apply()
    {
        currentState.Apply();
    }
}
