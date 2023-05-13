using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.Image;

public class Bot : Character
{
    public GameObject TargetIcon;


    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float sphereRadius = 10f;
    private int layerMask = -1;
    private Vector3 pos;
    [SerializeField] private float idleTimeCounter;
    private float idleTime;

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
    }
    public override void Update()
    {
        base.Update();
        //ShowTargetIcon();
    }

    //===========Patrolling============== *TODO
    //public override void IdleToPatrol()
    //{
    //    idleTimeCounter -= Time.deltaTime;
    //    if (idleTimeCounter <= 0)
    //    {
    //        ChangeState(PatrolState);
    //        idleTimeCounter = idleTime;
    //    }
    //}
    public override void Patrol()
    {
        agent.isStopped = false;
        agent.SetDestination(pos);
    }
    public override void FindDirection()
    {
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
        ChangeState(IdleState);
    }

    //private void ShowTargetIcon()
    //{
    //    if (gameObject == Player.target)
    //    {
    //        TargetIcon.SetActive(true);
    //    }
    //    else
    //    {
    //        TargetIcon.SetActive(false);
    //    }
    //}
}
