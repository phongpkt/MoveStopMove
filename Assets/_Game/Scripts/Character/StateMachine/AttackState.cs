using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_ATTACK);
    }
    public void OnExecute(Character character)
    {

    }
    public void OnExit(Character character)
    {

    }
}
