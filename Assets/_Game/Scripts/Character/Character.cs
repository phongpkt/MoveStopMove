using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;
using static UnityEditor.Progress;

public enum Hats { arrow, cowboy, crown, ear, hat, hat_cap, hat_yellow, headPhone, rau, horn };
public enum Pants { _default, batman, chambi, comy, dabao, onion, pokemon, rainbow, skull, vantim };
public enum Shields { khien, shield };
public enum FullSets { devil, angel, witch, deadpool, thor, _default};
public class Character : GameUnit
{
    //Set target
    public List<Character> Targets = new List<Character>();
    [HideInInspector] public Character currentTarget;
    private Vector3 targetDirection;

    //UI
    [HideInInspector] public Character currentAttacker;
    [HideInInspector] public string characterName;

    //Skin
    [HideInInspector] public Hats hat;
    [HideInInspector] public Pants pant;
    [HideInInspector] public Shields shield;
    [HideInInspector] public FullSets fullSet;
    [SerializeField] private HatData[] hatData;
    [SerializeField] private ShieldData[] shieldData;
    [SerializeField] private PantData[] pantData;
    [SerializeField] private FullsetData[] fullsetData;
    [SerializeField] public Transform headPosition;
    [SerializeField] public Transform backPosition;
    [SerializeField] public Transform shieldPosition;
    [SerializeField] public Transform tailPosition;
    [SerializeField] public SkinnedMeshRenderer pantRenderer;
    [SerializeField] public SkinnedMeshRenderer skinRenderer;


    //Character data
    public Rigidbody rb;
    [HideInInspector] public float speed;
    [HideInInspector] public float attackIntervalTimer;
    [HideInInspector] public float attackInterval;
    public int point;
    [HideInInspector] private int pointToGrow;
    private Vector3 increaseSize = new Vector3(0.2f, 0.2f, 0.2f);
    public SphereCollider attackRangeCollider;
    [HideInInspector] public float attackRangeRadius;

    //Weapons
    public Weapon equipedWeapon;
    [HideInInspector] public enum Weapon { Arrow, Hammer, Knife, Candy, Boomerang }
    [SerializeField] public Transform firePoint;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected WeaponData[] weaponsList;
    [HideInInspector] public int currentWeapon;
    [HideInInspector] public WeaponManager weaponInHand;

    //Character action bool
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public bool hasUlti;

    //Animation
    public Animator animator;
    private string currentAnim;

    //StateMachine
    protected IState currentState;
    public IdleState IdleState = new();
    public RunState RunState = new();
    public PatrolState PatrolState = new();
    public AttackState AttackState = new();
    public UltiState UltiState = new();
    public DeadState DeadState = new();
    public WinState WinState = new();

    public virtual void OnEnable()
    {
        OnInit();
    }
    public override void OnInit()
    {
        speed = 10f;
        point = 0;
        pointToGrow = 2;
        attackInterval = 2f;
        attackIntervalTimer = attackInterval;
        attackRangeRadius = attackRangeCollider.radius;
        isDead = false;
        ChangeState(IdleState);
    }

    public virtual void Update()
    {
        //Pause before hit play
        if (GameManager.Instance.IsState(GameState.GamePlay))
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
    public virtual void FindPosition() { }
    #endregion

    //===========Equip + Change Weapon==============
    #region Equip + Change Weapon
    public virtual void GetWeapon(int index)
    {
        for(int i=0; i<weaponsList.Length; i++)
        {
            if (weaponsList[i].index == index && weaponsList[i] != null)
            {
                Instantiate(weaponsList[i].model.gameObject, weaponHolder.transform);
                weaponInHand = weaponsList[i].model;
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
    public virtual void EquipWeapon(Weapon equipedWeapon)
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
    #region Equip + Change Clothes
    public virtual void EquipHat(Hats hat, int hatIndex)
    {
        if(PlayerPrefs.GetInt("fullset") >= 0)
        {
            EquipFullset(FullSets._default, 5); //default
        }
        switch (hat)
        {
            case Hats.rau:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.crown:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.cowboy:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.ear:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.hat:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.hat_cap:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.hat_yellow:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.headPhone:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.arrow:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
            case Hats.horn:
                ResetHead();
                ChangeHat(hatIndex);
                PlayerPrefs.SetInt("hat", hatIndex);
                PlayerPrefs.Save();
                break;
        }
    }
    public void EquipPant(Pants pant,int pantIndex)
    {
        switch (pant)
        {
            case Pants.batman:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.chambi:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.comy:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.dabao:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.onion:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.pokemon:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.rainbow:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.skull:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants.vantim:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
            case Pants._default:
                ChangePants(pantIndex);
                PlayerPrefs.SetInt("pant", pantIndex);
                PlayerPrefs.Save();
                break;
        }
    }
    public void EquipShield(Shields shield, int shieldIndex)
    {
        switch (shield)
        {
            case Shields.khien:
                ResetShield();
                ChangeShield(shieldIndex);
                PlayerPrefs.SetInt("shield", shieldIndex);
                PlayerPrefs.Save();
                break;
            case Shields.shield:
                ResetShield();
                ChangeShield(shieldIndex);
                PlayerPrefs.SetInt("shield", shieldIndex);
                PlayerPrefs.Save();
                break;
        }
    }
    public void EquipFullset(FullSets fullset, int fullsetIndex)
    {
        ResetAllClothes();
        switch (fullset)
        {
            case FullSets.devil:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
            case FullSets.angel:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
            case FullSets.witch:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
            case FullSets.deadpool:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
            case FullSets.thor:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
            case FullSets._default:
                ChangeSkin(fullsetIndex);
                PlayerPrefs.SetInt("fullset", fullsetIndex);
                PlayerPrefs.Save();
                break;
        }
    }
    private void ChangeHat(int _index)
    {
        foreach (var item in hatData)
        {
            if (item != null && item.index == _index)
            {
                Instantiate(item.model, headPosition);
            }
        }
    }
    private void ChangePants(int _index)
    {
        foreach (var item in pantData)
        {
            if (item != null && item.index == _index)
            {
                pantRenderer.material = item.material;
            }
        }
    }
    private void ChangeShield(int _index)
    {
        foreach (var item in shieldData)
        {
            if (item != null && item.index == _index)
            {
                Instantiate(item.model, shieldPosition);
            }
        }
    }
    private void ChangeSkin(int _index)
    {
        foreach (var item in fullsetData)
        {
            if (item.index == _index)
            {
                if (item.Head != null)
                {
                    Instantiate(item.Head, headPosition);
                }
                if (item.Tail != null)
                {
                    Instantiate(item.Tail, tailPosition);
                }
                if (item.Back != null)
                {
                    Instantiate(item.Back, backPosition);
                }
                if (item.LeftHand != null)
                {
                    Instantiate(item.LeftHand, shieldPosition);
                }
                skinRenderer.material = item.Skin;
            }
        }
    }
    public virtual void ResetAllClothes()
    {
        ResetHead();
        EquipPant(Pants._default, 9);
        ResetShield();
        ResetBack();
        ResetTail();
    }
    public virtual void ResetHead()
    {
        foreach (Transform item in headPosition)
        {
            Destroy(item.gameObject);
        }
    }
    public virtual void ResetShield()
    {
        foreach (Transform item in shieldPosition)
        {
            Destroy(item.gameObject);
        }
    }
    public virtual void ResetBack()
    {
        foreach (Transform item in backPosition)
        {
            Destroy(item.gameObject);
        }
    }
    public virtual void ResetTail()
    {
        foreach (Transform item in tailPosition)
        {
            Destroy(item.gameObject);
        }
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
        if(pointToGrow < 5)
        {
            pointToGrow += 3;
        }
        else
        {
            pointToGrow += 5;
        }


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
            if (hasUlti)
            {
                ChangeState(UltiState);
            }
            else
            {
                ChangeState(AttackState);
            }
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
        targetDirection = currentTarget.transform.position - transform.position;
        weaponInHand.Shoot(this, targetDirection);
        weaponHolder.SetActive(false);
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
        Targets.Clear();
    }
    #endregion

    //===========GameOver==============
    #region GameOver
    public virtual void Win() { }
    public virtual void Lose() { }
    #endregion

    //===========Die==============
    #region Die
    public virtual void Hit()
    {
        isHit = true;
        if (isHit)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDead = true;
        ChangeState(DeadState);
        Targets.Clear();
    }
    public override void OnDespawn() { }
    public virtual void DespawnWhenDie() { }
    #endregion

    //===========Ulti==============
    #region Ulti Chest
    public virtual void ChestBoost()
    {
        hasUlti = true;
    }
    public virtual void Ulti()
    {
        if(hasUlti)
        {
            targetDirection = currentTarget.transform.position - transform.position;
            weaponInHand.Ulti(this, targetDirection);
            weaponHolder.SetActive(false);
        }
    }
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
