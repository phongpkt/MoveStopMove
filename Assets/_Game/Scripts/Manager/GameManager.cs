    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, GameOver, GameWin, GamePause }

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    public void ChangeState(GameState state)
    {
        this.gameState = state;
    }
    public bool IsState(GameState state) => state == this.gameState;

    public int goldPerStage;
    public int totalPlayerGold;
    private void Awake()
    {
        totalPlayerGold = PlayerPrefs.GetInt("totalPlayerGold", 100000);
        goldPerStage = 0;
        gameState = GameState.MainMenu;
    }
    public void GetGoldAfterStage(int point)
    {
        int total = Constants.GOLD_PER_KILL * point;
        goldPerStage = goldPerStage + total + Constants.GOLD_PER_LEVEL;
        totalPlayerGold += goldPerStage;
        SaveGold();
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("totalPlayerGold", totalPlayerGold);
        PlayerPrefs.Save();
    }
}
