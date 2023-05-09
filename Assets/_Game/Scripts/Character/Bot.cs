using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Bot : Character
{
    //not active = dead
    public bool IsActive { get; set; }
    public NavMeshAgent agent { get; set; }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void Moving()
    {

    }
}
