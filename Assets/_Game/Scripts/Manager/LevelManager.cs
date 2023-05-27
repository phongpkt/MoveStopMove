using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static GameManager;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    public UnityAction onCharacterDie;
    
    public List<Bot> enemyCounter = new List<Bot>();
    public int totalBotAmount;
    
    private void Awake()
    {
        totalBotAmount = 20;
    }
    public void OnFinishGame()
    {
        if(totalBotAmount == 0)
        {
            GameManager.Instance.gameState = GameState.GameWin;
            SimplePool.CollectAll();
            enemyCounter.Clear();
        }
    }
    public void CharacterDie()
    {
        if (onCharacterDie != null)
        {
            onCharacterDie();
        }
    }

}
