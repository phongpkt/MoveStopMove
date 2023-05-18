using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { MainMenu, GamePlay, GameOver, GameWin, GamePause }
    public GameState gameState;

    public int goldPerStage;
    public int totalPlayerGold;
    private void Awake()
    {
        totalPlayerGold = PlayerPrefs.GetInt("totalPlayerGold", 0);
        goldPerStage = 0;
        gameState = GameState.MainMenu;
    }
    public void IncreaseGoldWhenKill()
    {
        goldPerStage += Constants.GOLD_PER_KILL;
    }

    public void GetGoldAfterStage()
    {
        goldPerStage += Constants.GOLD_PER_LEVEL;
        totalPlayerGold += goldPerStage;
        SaveGold();
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("totalPlayerGold", totalPlayerGold);
        PlayerPrefs.Save();
    }
}
