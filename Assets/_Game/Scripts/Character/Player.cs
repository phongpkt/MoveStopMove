using System;
using System.Threading.Tasks;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private GameObject _joystick;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject loseUI;

    private float horizontal;
    private float vertical;
    private Vector3 direction;
    
    [HideInInspector] public static Character target;
    public override void OnInit()
    {
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
        int currentSkin = PlayerPrefs.GetInt("fullset");
        if(currentSkin == 5)
        {
            EquipHat((Hats)currentHat, currentHat);
            EquipPant((Pants)currentPant, currentPant);
        }
        else
        {
            EquipFullset((FullSets)currentSkin, currentSkin);
        }
    }
    public override void Update()
    {
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
        AudioController.Instance.PlayWhenAttack();
    }
    public override void Win()
    {
        if(GameManager.Instance.IsState(GameState.GameWin))
        {
            CloseUI();
            ChangeState(WinState);
            GameManager.Instance.GetGoldAfterStage(point);
            victoryUI.SetActive(true);
            AudioController.Instance.PlayWhenWin();
        }
    }
    public override void Hit()
    {
        base.Hit();
        CloseUI();
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
            AudioController.Instance.StopBackgroundMusic();
            GameManager.Instance.GetGoldAfterStage(point);
            loseUI.SetActive(true);
            AudioController.Instance.PLayWhenLose();
        }
    }
    private void CloseUI()
    {
        _joystick.SetActive(false);
        joystick.input = Vector2.zero;
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
