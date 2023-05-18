using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Player player;
    public Bot botPrefab;
    public Transform mapRoot;

    public int botOnMap = 9;

    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;

    public void CheckNumberOfEnemies()
    {
        if (LevelManager.Instance.enemyCounter.Count < botOnMap && LevelManager.Instance.totalBotAmount >= botOnMap)
        {
            SpawnEnemy();
        }
    }
    private void Start()
    {
        LevelManager.Instance.onCharacterDie += CheckNumberOfEnemies;
        SimplePool.Preload(botPrefab, botOnMap, mapRoot);
        OnInit();
    }
    private void OnInit()
    {
        for (int i = 0; i < botOnMap; i++)
        {
            SpawnEnemy();
        }
    }
    private void OnDestroy()
    {
        LevelManager.Instance.onCharacterDie -= CheckNumberOfEnemies;
    }
    private void SpawnEnemy()
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
        LevelManager.Instance.enemyCounter.Add(bot);
    }
}
