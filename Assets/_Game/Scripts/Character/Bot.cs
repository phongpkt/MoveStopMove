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

    private float idleTimeCounter;
    private float idleTime;
    private double reactionTimer;

    public override void OnEnable()
    {
        base.OnEnable();
        idleTimeCounter = 0;
    }
    public override void OnInit()
    {
        agent = GetComponent<NavMeshAgent>();
        targetCircle.SetActive(false);
        base.OnInit();
    }
    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }
    //===========Equip Weapon==============
    public override void EquipWeapon()
    {
        equipedWeapon = (Weapon)UnityEngine.Random.Range(0, 4);
        base.EquipWeapon();
    }

    //===========Patrolling==============
    public override void Moving()
    {
        idleTimeCounter += Time.deltaTime;
        if (idleTimeCounter >= idleTime)
        {
            ChangeState(PatrolState);
            idleTimeCounter = 0;
        }
    }
    public override void Patrol()
    {
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(pos);
        ReactionTime();
    }
    public override void FindDirection()
    {
        idleTime = UnityEngine.Random.Range(0f, 2f);
        reactionTimer = UnityEngine.Random.Range(0.5f, 1.5f);

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
    private void ReactionTime()
    {
        reactionTimer -= Time.deltaTime;
        if (reactionTimer <= 0)
        {
            ChangeState(IdleState);
        }
    }
    //===========Die==============
    public override void Die()
    {
        base.Die();
    }
    public override void DespawnWhenDie()
    {
        SimplePool.Despawn(this);
        LevelManager.Instance.enemyCounter.Remove(this);
        LevelManager.Instance.totalBotAmount--;
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
