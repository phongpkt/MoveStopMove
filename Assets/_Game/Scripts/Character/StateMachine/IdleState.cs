using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_IDLE);
        character.CheckAroundCharacters();
    }
    public void OnExecute(Character character) 
    {
        character.Moving();
    }

    public void OnExit (Character character)
    {

    }
}
