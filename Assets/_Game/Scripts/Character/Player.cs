using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] protected GameObject inGameCanvas;
    [SerializeField] private GameObject _joystickUI;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject loseUI;

    private float horizontal;
    private float vertical;
    private Vector3 direction;

    public static Character target;
    public override void OnInit()
    {
        inGameCanvas.SetActive(false);
        //TODO: nen viet thanh mot datamanager rieng
        dataManager();
        base.OnInit();
    }
    private void dataManager()
    {
        int currentWeapon = PlayerPrefs.GetInt("currentWeaponIndex");
        equipedWeapon = (Weapon)PlayerPrefs.GetInt("equipedWeapon", currentWeapon);
        characterName = PlayerPrefs.GetString("playerName", "Player");
        int currentHat = PlayerPrefs.GetInt("hat");
        EquipHat((Hats)currentHat, currentHat);
    }
    public override void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            inGameCanvas.SetActive(true);
        }
        base.Update();
        //if(Targets.Count != 0 )
        //    target = Targets[0];
        //else
        //    target = null;

        target = Targets.Count > 0 ? Targets[0] : null;
    }
    private void FixedUpdate()
    {
        Moving();
    }
    public override void Win()
    {
        if(GameManager.Instance.IsState(GameState.GameWin))
        {
            CloseUI();
            ChangeState(WinState);
            GameManager.Instance.GetGoldAfterStage();
            victoryUI.SetActive(true);
        }
    }
    public override async void Die()
    {
        base.Die();
        if (isDead)
        {
            CloseUI();
            GameManager.Instance.ChangeState(GameState.GameOver);
            await Task.Delay(TimeSpan.FromSeconds(1));
            gameObject.SetActive(false);
            Lose();
        }
    }
    public void Lose()
    {
        if(GameManager.Instance.IsState(GameState.GameOver))
        {
            loseUI.SetActive(true);
        }
    }
    private void CloseUI()
    {
        _joystickUI.SetActive(false);
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
