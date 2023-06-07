using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static GameManager;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    public Character player;
    public Transform playerTf;

    public List<Bot> enemyCounter = new List<Bot>();
    public int totalBotAmount;
    public int totalBotOnMap;
    //For UI
    public int total;

    private Vector3 randomPosition;
    private Vector3 randomChestPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;
    
    //Chest
    public List<ChestUlti> chestUltiCounter = new List<ChestUlti>();
    public List<ChestSpeed> chestSpeedCounter = new List<ChestSpeed>();
    private int totalChestOnMap;
    private float spawnChestTime = 5f;
    private float spawnChestTimer;

    private void Awake()
    {
        LevelData();
    }
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            CheckNumberOfChest();
        }
    }
    private void LevelData()
    {
        totalChestOnMap = 3;
        totalBotAmount = 50;
        totalBotOnMap = 10;
        total = totalBotAmount;
        spawnChestTimer = spawnChestTime;
    }
    public void OnFinishGame()
    {
        if(totalBotAmount == 0)
        {
            GameManager.Instance.ChangeState(GameState.GameWin);
            SimplePool.CollectAll();
            enemyCounter.Clear();
            player.Win();
        }
        else
        {
            CheckNumberOfEnemies();
        }
    }
    public void LoadLevel(int level)
    {

    }
    public void OnPlay()
    {
        OnInit();
        PlayAudio();
    }
    public void OnInit()
    {
        for (int i = 0; i < totalBotOnMap; i++)
        {
            SpawnBot();
        }
    }
    public void OnRetry()
    {
        //reset game
        SimplePool.CollectAll();
        enemyCounter.Clear();
        chestUltiCounter.Clear();
        LevelData();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        //reset player
        playerTf.position = new Vector3(0, 0, -5f);
        playerTf.rotation = Quaternion.Euler(0f, 180f, 0f);
        playerTf.localScale = new Vector3(1, 1, 1);
        player.gameObject.SetActive(true);
        player.OnInit();
        //resetcamera
        Camera.main.transform.position = new Vector3(0f, 8f, -20f);
        Camera.main.transform.rotation = Quaternion.Euler(13f, 0f, 0f);

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
    private void SpawnUltiChest()
    {
        xPos = Random.Range(-50, 50);
        zPos = Random.Range(-50, 50);
        position = new Vector3(xPos, 0, zPos);

        NavMeshHit closestHit;
        NavMesh.SamplePosition(position, out closestHit, 180f, NavMesh.AllAreas);
        randomChestPosition = closestHit.position;
        ChestUlti ulti = SimplePool.Spawn<ChestUlti>(PoolType.Ulti, randomChestPosition, Quaternion.identity);
        chestUltiCounter.Add(ulti);
    }
    private void SpawnSpeedChest()
    {
        xPos = Random.Range(-50, 50);
        zPos = Random.Range(-50, 50);
        position = new Vector3(xPos, 0, zPos);

        NavMeshHit closestHit;
        NavMesh.SamplePosition(position, out closestHit, 180f, NavMesh.AllAreas);
        randomChestPosition = closestHit.position;
        ChestSpeed speed = SimplePool.Spawn<ChestSpeed>(PoolType.Speed, randomChestPosition, Quaternion.identity);
        chestSpeedCounter.Add(speed);
    }
    private void CheckNumberOfChest()
    {
        if (chestUltiCounter.Count < totalChestOnMap)
        {
            spawnChestTimer -= Time.deltaTime;
            if (spawnChestTimer <= 0)
            {
                SpawnUltiChest();
                spawnChestTimer = spawnChestTime;
            }
        }
    }
    private void PlayAudio()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            AudioController.Instance.PlayOnStart();
        }
    }
}
