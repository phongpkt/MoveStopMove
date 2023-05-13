using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_ATTACK);
        character.LookAtTarget();
        character.Attack();
    }
    public void OnExecute(Character character)
    {
        character.ResetAttack();
    }
    public void OnExit(Character character)
    {
        character.StopAttack();
    }
}
