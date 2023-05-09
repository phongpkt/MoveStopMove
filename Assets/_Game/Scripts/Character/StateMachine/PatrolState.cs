using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_RUN);
    }
    public void OnExecute(Character character)
    {
        if (Input.GetMouseButtonUp(0))
        {
            character.ChangeState(character.IdleState);
        }
    }
    public void OnExit(Character character)
    {
        character.ChangeAnim(Constants.ANIM_IDLE);
        character.ChangeAnim(Constants.ANIM_ATTACK);
    }
}
