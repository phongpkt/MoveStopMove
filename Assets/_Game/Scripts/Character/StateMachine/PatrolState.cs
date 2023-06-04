using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_RUN);
        character.FindPosition();
    }
    public void OnExecute(Character character)
    {
        character.Patrol();
    }
    public void OnExit(Character character)
    {
        character.StopPatrol();
    }
}
