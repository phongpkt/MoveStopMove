using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    public Character Owner;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Constants.CHARACTER_TAG))
        {
            Character target = Cache.GetCharacter(col);
            if (target != Owner)
            {
                Owner.Targets.Add(target);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(Constants.CHARACTER_TAG))
        {
            Character target = Cache.GetCharacter(col);
            if (target != Owner)
            {
                if (Owner.Targets.Contains(target)) 
                {
                    Owner.Targets.Remove(target);
                }
            }
        }
    }
}
