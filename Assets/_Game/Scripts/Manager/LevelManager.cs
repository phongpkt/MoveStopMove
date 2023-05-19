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
    
    public List<Level> levels = new List<Level>();
    private Level currentLevel;
    private int levelIndex;

    private void Awake()
    {
        totalBotAmount = 20;
    }
    //private void Start()
    //{
    //    LoadLevel(levelIndex);
    //    OnInit();
    //}
    //public void OnInit() 
    //{

    //}
    //public void OnRetry()
    //{
    //    LoadLevel(levelIndex);
    //    OnDespawn();
    //    OnInit();
    //}
    //public void LoadLevel(int level) 
    //{
    //    if (currentLevel != null)
    //    {
    //        Destroy(currentLevel.gameObject);
    //    }
    //}
    public void OnStartGame()
    {
        GameManager.Instance.gameState = GameState.GamePlay;
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
