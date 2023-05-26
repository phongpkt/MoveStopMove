using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_DANCE);
    }
    public void OnExecute(Character character)
    {

    }
    public void OnExit(Character character)
    {

    }
}
