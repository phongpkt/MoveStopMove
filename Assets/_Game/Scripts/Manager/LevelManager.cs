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
    public Transform playerTf;

    public UnityAction onCharacterDie;
    public List<Bot> enemyCounter = new List<Bot>();
    public int totalBotAmount;
    public int totalBotOnMap;


    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;

    private void Awake()
    {
        totalBotAmount = 20;
    }
    private void Start()
    {
        OnInit();
        OnPlay();
    }
    public void OnFinishGame()
    {
        if(totalBotAmount == 0)
        {
            GameManager.Instance.ChangeState(GameState.GameWin);
            SimplePool.CollectAll();
            enemyCounter.Clear();
        }
    }
    public void LoadLevel(int level)
    {

    }
    public void OnPlay()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            CheckNumberOfEnemies();
        }
    }
    public void OnInit()
    {
        for (int i = 0; i < totalBotOnMap; i++)
        {
            SpawnBot();
        }
    }
    public void OnDespawn()
    {

    }
    private void CheckNumberOfEnemies()
    {
        if (enemyCounter.Count < totalBotOnMap && totalBotAmount >= totalBotOnMap)
        {
            SpawnBot();
        }
    }
    private void SpawnBot()
    {
        do
        {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            position = new Vector3(xPos, 0, zPos);

            NavMeshHit closestHit;
            NavMesh.SamplePosition(position, out closestHit, 100f, NavMesh.AllAreas);
            randomPosition = closestHit.position;

        } while (Vector3.Distance(randomPosition, playerTf.position) < 7f);

        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, randomPosition, Quaternion.identity);
        enemyCounter.Add(bot);
        bot.deathAction = DespawnBot;
    }
    private void DespawnBot(Bot bot)
    {
        enemyCounter.Remove(bot);
        totalBotAmount -= 1;
        //check game
        OnFinishGame();
    }
}
