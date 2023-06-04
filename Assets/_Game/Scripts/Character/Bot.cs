using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Character;
using static UnityEngine.UI.Image;

public class Bot : Character
{
    public enum EnemyName { Mitchell, Roy, Parker, Steve, Barnes, Washington, Walker, Michael, Jackson, Patterson, Griffin, Thomas, Ramirez, Bryant, Young }
    public EnemyName b_name;
    [SerializeField] private CharacterSkinData characterSkinData;
    [SerializeField] private List<Material> assignedSkin;

    public GameObject targetCircle;
    public Indicator wayPointIndicator;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float sphereRadius = 50f;
    private Vector3 pos;

    private float idleTimeCounter;
    private float idleTime;
    private float patrolTime;

    public UnityAction<Bot> deathAction;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetCircle.SetActive(false);
        idleTimeCounter = 0;
        GetRandomName();
        EquipWeapon(equipedWeapon);
    }
    public override void OnInit()
    {
        base.OnInit();
        SetUpIndicator();
        Material randomSkin = GetRandomSkin();
        skinRenderer.material = randomSkin;
        for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
        {
            IncreasePoint();
        }
    }
    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }
    //==========In game UI==============
    #region In game UI
    public void SetUpIndicator() 
    {
        wayPointIndicator = SimplePool.Spawn<Indicator>(PoolType.Indicator, transform.position, Quaternion.identity);
        wayPointIndicator.OnInit(this, transform);
    }
    public void GetRandomName()
    {
        b_name = (EnemyName)UnityEngine.Random.Range(0, 10);
        characterName = b_name.ToString();
    }
    #endregion
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
        ResetPatrolTime();
    }
    public override void FindPosition()
    {
        patrolTime = UnityEngine.Random.Range(0.5f, 1.5f);
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * sphereRadius;
        randDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDirection, out hit, sphereRadius, Constants.LAYER_MASK);

        Vector3 newPos = hit.position;
        pos = newPos;
    }
    private void ResetPatrolTime()
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
    }
    #endregion
    //===========Die==============
    #region Die
    public override async void Die()
    {
        base.Die();
        await Task.Delay(TimeSpan.FromSeconds(1.5));
        DespawnWhenDie();
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
        if (this == Player.target)
        {
            targetCircle.SetActive(true);
        }
        else
        {
            targetCircle.SetActive(false);
        }
    }
    private Material GetRandomSkin()
    {
        if (assignedSkin == null)
            assignedSkin = new List<Material>();

        if (assignedSkin.Count >= characterSkinData.skinColor.Count)
        {
            Debug.Log("All available color have been assigned.");
        }

        int randomIndex = UnityEngine.Random.Range(0, characterSkinData.skinColor.Count);
        Material randomSkin = characterSkinData.skinColor[randomIndex];
        assignedSkin.Add(randomSkin);
        return randomSkin;
    }
}
