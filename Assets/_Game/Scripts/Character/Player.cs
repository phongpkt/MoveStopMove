using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private GameObject _joystickUI;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject victoryUI;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    public static Character target;
    public override void OnInit()
    {
        equipedWeapon = (Weapon)PlayerPrefs.GetInt("equipedWeapon", 0);
        characterName = PlayerPrefs.GetString("playerName", "Player");
        base.OnInit();
    }
    public override void Update()
    {
        base.Update();
        if(Targets.Count != 0 )
            target = Targets[0];
        else
            target = null;
    }
    private void FixedUpdate()
    {
        Moving();
    }
    public override void Win()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.GameWin)
        {
            CloseUI();
            ChangeState(WinState);
            GameManager.Instance.GetGoldAfterStage();
            victoryUI.SetActive(true);
        }
    }
    public override void Die()
    {
        base.Die();
        if (isDead)
        {
            CloseUI();
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
        }
    }
    private void CloseUI()
    {
        _joystickUI.SetActive(false);
    }

    public override void DespawnWhenDie()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.CharacterDie();
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
