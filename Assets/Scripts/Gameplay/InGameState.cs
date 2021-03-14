using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : GameplayState
{
    HelicopterBehaviour helicopterBehaviour;
    public InGameState(GameplayManager manager) : base(manager)
    {
        helicopterBehaviour = gameplayManager.player.GetComponent<HelicopterBehaviour>();
        helicopterBehaviour.StartFlying();

        gameplayManager.endGameMenu.gameObject.SetActive(false);
    }
    public override void Apply()
    {
        gameplayManager.IncreaseDifficulty();

        if(helicopterBehaviour.isDead)
        {
            gameplayManager.stateMachine.SetState(new GameOverState(gameplayManager));
        }
    }
}
