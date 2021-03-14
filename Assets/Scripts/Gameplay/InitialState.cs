using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialState : GameplayState
{
    HelicopterBehaviour helicopterBehaviour;
    public InitialState(GameplayManager manager) : base(manager)
    {
        helicopterBehaviour = gameplayManager.player.GetComponent<HelicopterBehaviour>();

        helicopterBehaviour.Reposition();
        gameplayManager.ResetDifficulty();
    }

    public override void Apply()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameplayManager.stateMachine.SetState(new InGameState(gameplayManager));
        }
    }    
}
