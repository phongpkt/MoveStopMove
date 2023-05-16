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
    public Player player;
    public Bot botPrefab;

    private Level currentLevel;
    private int levelIndex;

    public List<Bot> enemyCounter = new List<Bot>();
    public int totalBotAmount;

    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;

    private void Awake()
    {
        //levelIndex = PlayerPrefs.GetInt("Level", 0);
        totalBotAmount = 9;
    }
    private void Start()
    {
        onCharacterDie += CheckNumberOfEnemies;
        LoadLevel(levelIndex);
        OnInit();
    }
    public void OnInit() 
    {
        for (int i = 0; i < totalBotAmount; i++)
        {
            SpawnEnemy();
        }
    }
    public void OnRetry()
    {
        LoadLevel(levelIndex);
        OnDespawn();
        OnInit();
    }
    public void OnDespawn()
    {
        SimplePool.CollectAll();
        enemyCounter.Clear();
    }
    public void LoadLevel(int level) 
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
    }
    public void OnStartGame()
    {
        GameManager.Instance.gameState = GameState.GamePlay;
    }
    public void OnFinishGame()
    {
        if (enemyCounter.Count == 0)
        {
            GameManager.Instance.gameState = GameState.GameWin;
        }
    }
    public void CharacterDie()
    {
        if (onCharacterDie != null)
        {
            onCharacterDie();
        }
    }
    public void SpawnEnemy()
    {
        do
        {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            position = new Vector3(xPos, 0, zPos);

            NavMeshHit closestHit;
            NavMesh.SamplePosition(position, out closestHit, 100f, NavMesh.AllAreas);
            randomPosition = closestHit.position;

        } while (Vector3.Distance(randomPosition, player.transform.position) < 7f);
        Bot bot = SimplePool.Spawn<Bot>(botPrefab, randomPosition, Quaternion.identity);
        enemyCounter.Add(bot);
    }
    public void CheckNumberOfEnemies()
    {
        if (enemyCounter.Count < totalBotAmount && currentLevel.botAmount > 0)
        {
            SpawnEnemy();
        }
    }
    private void OnDestroy()
    {
        onCharacterDie -= CheckNumberOfEnemies;
    }
}
