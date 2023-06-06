using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : Character
{
    [SerializeField] protected GameObject inGameCanvas;
    [SerializeField] private GameObject _joystick;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject loseUI;

    [SerializeField] private AudioController audioController;


    private float horizontal;
    private float vertical;
    private Vector3 direction;
    
    public static Character target;
    public override void OnInit()
    {
        //inGameCanvas.SetActive(false);
        DataManager();
        rb.velocity = Vector3.zero;
        base.OnInit();
    }
    private void DataManager()
    {
        characterName = PlayerPrefs.GetString("playerName", "Player");
        int currentWeapon = PlayerPrefs.GetInt("currentWeaponIndex");
        equipedWeapon = (Weapon)PlayerPrefs.GetInt("equipedWeapon", currentWeapon);
        EquipWeapon(equipedWeapon);
        int currentHat = PlayerPrefs.GetInt("hat");
        int currentPant = PlayerPrefs.GetInt("pant");
        //TODO: shield luon bi spawn khi mo game
        //int currentShield = PlayerPrefs.GetInt("shield");
        int currentSkin = PlayerPrefs.GetInt("fullset");
        if(currentSkin == 5) //default skin
        {
            EquipHat((Hats)currentHat, currentHat);
            EquipPant((Pants)currentPant, currentPant);
            //EquipShield((Shields)currentShield, currentShield);
        }
        else
        {
            EquipFullset((FullSets)currentSkin, currentSkin);
        }
    }
    public override void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            inGameCanvas.SetActive(true);
        }
        base.Update();
        target = Targets.Count > 0 ? Targets[0] : null;
    }
    private void FixedUpdate()
    {
        Moving();
    }
    public override void Attack()
    {
        base.Attack();
        audioController.PlayWhenAttack();
    }
    public override void Win()
    {
        if(GameManager.Instance.IsState(GameState.GameWin))
        {
            CloseUI();
            ChangeState(WinState);
            GameManager.Instance.GetGoldAfterStage(point);
            victoryUI.SetActive(true);
            audioController.PlayWhenWin();
        }
    }
    public override void Hit()
    {
        base.Hit();
        CloseUI();
        audioController.PLayWhenHit();
    }
    public override async void Die()
    {
        GameManager.Instance.ChangeState(GameState.GameOver);
        base.Die();
        await Task.Delay(TimeSpan.FromSeconds(1.5));
        Lose();
    }
    public override void Lose()
    {
        this.gameObject.SetActive(false);
        if(GameManager.Instance.IsState(GameState.GameOver))
        {
            GameManager.Instance.GetGoldAfterStage(point);
            loseUI.SetActive(true);
            audioController.PLayWhenLose();
        }
    }
    private void CloseUI()
    {
        _joystick.SetActive(false);
    }
    public override void Moving()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        direction = (Vector3.forward * vertical + Vector3.right * horizontal) * speed;
        if(direction.magnitude != 0)
        {
            rb.velocity = direction;
            Vector3 lookDirection = direction;
            if (Mathf.Abs(lookDirection.magnitude) > 0.01f)
            {

                Quaternion rotate = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = rotate;
            }
            ChangeState(RunState);
        }
        else if(!isAttack)
        {
            rb.velocity = Vector3.zero;
            ChangeState(IdleState);
        }
    }
}
