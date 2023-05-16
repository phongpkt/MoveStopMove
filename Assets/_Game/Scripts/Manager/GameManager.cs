using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { MainMenu, GamePlay, GameOver, GameWin, GamePause }
    public GameState gameState;

    public int totalEnemies;
    private void Awake()
    {
        gameState = GameState.MainMenu;
    }
    public void CheckGameWin()
    {

    }
}
