using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_ULTI);
        character.LookAtTarget();
        character.Ulti();
    }
    public void OnExecute(Character character)
    {
        character.ResetAttack();
    }
    public void OnExit(Character character)
    {
        character.StopAttack();
        character.hasUlti = false;
    }
}
