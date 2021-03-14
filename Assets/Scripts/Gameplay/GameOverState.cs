using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameplayState
{
    HelicopterBehaviour helicopterBehaviour;
    public GameOverState(GameplayManager manager) : base(manager)
    {
        helicopterBehaviour = gameplayManager.player.GetComponent<HelicopterBehaviour>();
        gameplayManager.endGameMenu.gameObject.SetActive(true);
    }
    public override void Apply()
    {
        gameplayManager.endGameMenu.SetScore(gameplayManager.currentScore);
    }
}
