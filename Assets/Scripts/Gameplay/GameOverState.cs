using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameplayState
{
    public GameOverState(GameplayManager manager) : base(manager)
    {
        gameplayManager.endGameMenu.gameObject.SetActive(true);
    }
    public override void Apply()
    {
        gameplayManager.endGameMenu.SetScore();
    }
}
