using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Bot : Character
{
    public enum EnemyName { Mitchell, Roy, Parker, Steve, Barnes, Washington, Walker, Michael, Jackson, Patterson, Griffin, Thomas, Ramirez, Bryant, Young }
    public GameObject targetCircle;
    public Indicator wayPointIndicator;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float sphereRadius = 50f;
    private Vector3 pos;

    private float idleTimeCounter;
    private float idleTime;
    private double patrolTime;

    public UnityAction<Bot> deathAction;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetCircle.SetActive(false);
        idleTimeCounter = 0;
    }
    public override void OnInit()
    {
        SetUpIndicator();
        base.OnInit();
    }
    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }
    //==========In game UI==============
    public void SetUpIndicator() 
    {
        wayPointIndicator = SimplePool.Spawn<Indicator>(PoolType.Indicator, transform.position, Quaternion.identity);
        wayPointIndicator.OnInit(this, transform);
    }

    //===========Equip Weapon==============
    public override void EquipWeapon(Weapon _weapon)
    {
        _weapon = (Weapon)UnityEngine.Random.Range(0, 4);
        equipedWeapon = _weapon;
        base.EquipWeapon(equipedWeapon);
    }

    //===========Patrolling==============
    #region Patrol
    public override void Moving()
    {
        idleTime = UnityEngine.Random.Range(0f, 2f);
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
        SetIdleTime();
    }
    public override void FindDirection()
    {
        patrolTime = UnityEngine.Random.Range(0.5f, 1.5f);

        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * sphereRadius;
        randDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDirection, out hit, sphereRadius, Constants.LAYER_MASK);
        Vector3 newPos = hit.position;
        pos = newPos;
    }
    private void SetIdleTime()
    {
        patrolTime -= Time.deltaTime;
        if (patrolTime <= 0)
        {
            ChangeState(IdleState);
        }
    }
    public override void StopPatrol()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }
    #endregion
    //===========Die==============
    #region Die
    public override void Die()
    {
        base.Die();
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        wayPointIndicator.gameObject.SetActive(false);
    }
    public override void DespawnWhenDie()
    {
        OnDespawn();
        deathAction?.Invoke(this);
    }
    #endregion
    //===========Addition GamePlay==============
    private void ShowTargetIcon()
    {
        if (gameObject.GetComponent<Bot>() == Player.target)
        if (this == Player.target)
        {
            targetCircle.SetActive(true);
        }
        else
        {
            targetCircle.SetActive(false);
        }
    }
}
