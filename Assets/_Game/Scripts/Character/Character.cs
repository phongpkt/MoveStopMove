using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;
using static UnityEditor.Progress;

public class Character : GameUnit
{
    //Set target
    public List<Character> Targets = new List<Character>();
    [HideInInspector] public Character currentTarget;
    private Vector3 targetDirection;

    //UI
    [SerializeField] protected GameObject inGameCanvas;
    [HideInInspector] public Character currentAttacker;
    [HideInInspector] public string characterName;

    //Skin
    [SerializeField] public Transform headPosition;
    [SerializeField] public Transform backPosition;
    [SerializeField] public Transform shieldPosition;
    [SerializeField] public Transform tailPosition;
    [SerializeField] public SkinnedMeshRenderer pantRenderer;
    [SerializeField] public SkinnedMeshRenderer skinRenderer;

    //Character data
    public Rigidbody rb;
    public float speed;
    public Transform attackRangeScale;
    [HideInInspector] public float attackIntervalTimer;
    [HideInInspector] public float attackInterval;
    [HideInInspector] public int point;
    [HideInInspector] private int pointToGrow;
    private Vector3 increaseSize = new Vector3(0.2f, 0.2f, 0.2f);
    [HideInInspector] public float baseAttackRange;

    //Weapons
    public enum Weapon { Arrow, Hammer, Knife, Candy, Boomerang }
    public Weapon equipedWeapon;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected Weapons[] weaponsList;
    private int currentWeapon;
    public GameObject weaponInHand;

    //Character action bool
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public bool isDead;

    //Animation
    public Animator animator;
    private string currentAnim;

    //StateMachine
    protected IState currentState;
    public IdleState IdleState = new();
    public RunState RunState = new();
    public PatrolState PatrolState = new();
    public AttackState AttackState = new();
    public DeadState DeadState = new();
    public WinState WinState = new();


    public virtual void OnEnable()
    {
        OnInit();
        LevelManager.Instance.onCharacterDie += Win;
    }
    public virtual void OnDisable()
    {
        LevelManager.Instance.onCharacterDie -= Win;
    }
    public override void OnInit()
    {
        inGameCanvas.SetActive(false);
        speed = 10f;
        point = 0;
        pointToGrow = 5;
        baseAttackRange = 0.5f;
        attackInterval = 2f;
        attackIntervalTimer = attackInterval;
        currentWeapon = 0;
        baseAttackRange = attackRangeScale.GetComponent<SphereCollider>().radius;
        isDead = false;
        EquipWeapon();
        ChangeState(IdleState);
    }
    public override void OnDespawn() { }
    public virtual void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GamePlay)
        {
            inGameCanvas.SetActive(true);
        }
        if (GameManager.Instance.gameState == GameManager.GameState.GamePlay)
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }
    }
    //===========Moving==============
    #region Patrol + Moving
    public virtual void Moving() { }
    public virtual void Patrol() { }
    public virtual void ResetPatrol() { }
    public virtual void StopPatrol() { }
    public virtual void FindDirection() { }
    #endregion
    //===========Equip + Change Weapon==============
    #region Equip + Change Weapon
    public virtual void EquipWeapon()
    {
        SwitchWeapon(equipedWeapon);
    }
    public virtual void GetWeapon(int index)
    {
        for(int i=0; i<weaponsList.Length; i++)
        {
            if (weaponsList[i].index == index && weaponsList[i] != null)
            {
               
                weaponInHand = Instantiate(weaponsList[i].model, weaponHolder.transform);
                WeaponManager weaponManager = weaponInHand.GetComponent<WeaponManager>();
                weaponManager.firePoint = firePoint;
            }
        }
    }
    public virtual void RemoveWeapon()
    {
        if(weaponInHand != null)
        {
            Transform weapon = weaponHolder.transform.GetChild(0);
            Destroy(weapon.gameObject);
        }
    }
    public virtual void SwitchWeapon(Weapon equipedWeapon)
    {
        switch (equipedWeapon)
        {
            case Weapon.Arrow:
                currentWeapon = 0;
                RemoveWeapon();
                GetWeapon(currentWeapon);
                break;
            case Weapon.Hammer:
                currentWeapon = 1;
                RemoveWeapon();
                GetWeapon(currentWeapon);
                break;
            case Weapon.Knife:
                currentWeapon = 2;
                RemoveWeapon();
                GetWeapon(currentWeapon);
                break;
            case Weapon.Candy:
                currentWeapon = 3;
                RemoveWeapon();
                GetWeapon(currentWeapon);
                break;
            case Weapon.Boomerang:
                currentWeapon = 4;
                RemoveWeapon();
                GetWeapon(currentWeapon);
                break;
        }
    }
    #endregion
    //===========Change Clothes==============
    //#TODO
    #region Change Clothes
    public virtual void ChangeClothes()
    {

    }
    public virtual void ResetClothes()
    {
        Destroy(headPosition.gameObject);
        Destroy(tailPosition.gameObject);
        Destroy(shieldPosition.gameObject);
        Destroy(backPosition.gameObject);
    }
    #endregion
    //===========Increase Size + Attack range==============
    #region Grow
    public virtual void IncreasePoint()
    {
        point += 1;
        if (point >= pointToGrow)
        {
            Grow();
        }
    }
    public virtual void Grow()
    {
        gameObject.transform.localScale += increaseSize;
        pointToGrow += 5;
    }
    #endregion

    //===========Attack==============
    #region Attack
    public virtual void CheckAroundCharacters() 
    {
        if (Targets.Count != 0)
        {
            currentTarget = Targets[0];
            isAttack = true;
            ChangeState(AttackState);
        }
    }
    public virtual void LookAtTarget()
    {
        if (currentTarget != null)
        {
            Vector3 lookDirection = currentTarget.transform.position - transform.position;
            lookDirection.y = 0f;
            lookDirection = lookDirection.normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
    public virtual void Attack()
    {
        WeaponManager weaponManager = weaponInHand.GetComponent<WeaponManager>();
        targetDirection = currentTarget.transform.position - transform.position;
        weaponHolder.SetActive(false);
        weaponManager.Shoot(currentTarget, targetDirection);
    }
    public virtual void ResetAttack()
    {
        if (isAttack)
        {
            attackIntervalTimer -= Time.deltaTime;
            if (attackIntervalTimer <= 0)
            {
                ChangeState(IdleState);
                attackIntervalTimer = attackInterval;
            }
        }
    }
    public virtual void StopAttack()
    {
        weaponHolder.SetActive(true);
        isAttack = false;
        Targets.Remove(currentTarget);
        currentTarget = null;
    }
    public virtual void Hit()
    {
        isHit = true;
        if(isHit)
        {
            Die();
        }
    }
    #endregion
    //===========GameOver==============
    #region GameOver
    public virtual void Win() { }
    #endregion
    //===========Die==============
    #region Die
    public virtual async void Die()
    {
        isDead = true;
        Targets.Clear();
        ChangeState(DeadState);
        await Task.Delay(TimeSpan.FromSeconds(1.5));
        DespawnWhenDie();
    }
    public virtual void DespawnWhenDie() { }
    #endregion

    //===========Animation==============
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    //===========StateController==============
    public virtual void ChangeState(IState state)
    {
        if (currentState == state)
            return;
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
