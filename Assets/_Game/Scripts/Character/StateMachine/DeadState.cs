using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_DEAD);
    }
    public void OnExecute(Character character)
    {

    }
    public void OnExit(Character character)
    {

    }
}
