using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constants.ANIM_RUN);
    }
    public void OnExecute(Character character)
    {
        
    }
    public void OnExit(Character character)
    {
        
    }
}
