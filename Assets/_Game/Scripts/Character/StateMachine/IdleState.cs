using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_IDLE);
        //quet xung quanh co character nao khong
        //check list target > 0
        // neu list target > 0 thi chuyen sang state Attack()
    }
    public void OnExecute(Character character) 
    {
        if (Input.GetMouseButtonDown(0))
        {
            character.ChangeState(character.PatrolState);
        }
    }

    public void OnExit (Character character)
    {

    }
}
