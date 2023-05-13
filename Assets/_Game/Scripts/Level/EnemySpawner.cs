using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameUnit bot;
    private int enemiesOnMap = 10;
    
    private Vector3 randomPosition;
    private Vector3 position;
    private int xPos;
    private int zPos;
    private void Start()
    {
        for (int i = 0; i < enemiesOnMap; i++)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        do
        {
            xPos = Random.Range(-50, 50);
            zPos = Random.Range(-50, 50);
            position = new Vector3(xPos, 0, zPos);

            NavMeshHit closestHit;
            NavMesh.SamplePosition(position, out closestHit, 250f, NavMesh.AllAreas);
            randomPosition = closestHit.position;
            SimplePool.Spawn(bot, randomPosition, Quaternion.identity);

        } while (Vector3.Distance(randomPosition, player.position) < 7f);
    }
}
