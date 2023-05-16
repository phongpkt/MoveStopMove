using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.Image;

public class Bot : Character
{
    public GameObject targetCircle;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float sphereRadius = 50f;
    private int layerMask = -1;
    private Vector3 pos;
    [SerializeField] private float idleTimeCounter = 0;
    [SerializeField] private float idleTime;

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnInit()
    {
        base.OnInit();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        idleTime = UnityEngine.Random.Range(0f, 2f);
        targetCircle.SetActive(false);
    }
    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }
    //===========Patrolling==============
    public override void Moving()
    {
        idleTimeCounter += Time.deltaTime;
        if (idleTimeCounter > idleTime)
        {
            ChangeState(PatrolState);
            idleTimeCounter = 0;
        }
    }
    public override void Patrol()
    {
        agent.isStopped = false;
        agent.SetDestination(pos);
        if(!agent.hasPath || Targets.Count != 0)
        {
            ChangeState(IdleState);
        }
    }
    public override void FindDirection()
    {
        idleTime = UnityEngine.Random.Range(0f, 2f);
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * sphereRadius;
        randDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDirection, out hit, sphereRadius, layerMask);
        Vector3 newPos = hit.position;
        pos = newPos;
    }
    public override void StopPatrol()
    {
        agent.isStopped = true;
    }
    //===========Die==============
    public override void Die()
    {
        base.Die();
        LevelManager.Instance.CheckNumberOfEnemies();
    }
    public override void DespawnWhenDie()
    {
        SimplePool.Despawn(this);
        LevelManager.Instance.enemyCounter.Remove(this);
        LevelManager.Instance.CharacterDie();
        LevelManager.Instance.OnFinishGame();
    }
    //===========Addition GamePlay==============
    private void ShowTargetIcon()
    {
        if (gameObject.GetComponent<Bot>() == Player.target)
        {
            targetCircle.SetActive(true);
        }
        else
        {
            targetCircle.SetActive(false);
        }
    }
}
