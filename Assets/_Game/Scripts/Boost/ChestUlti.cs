using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestUlti : GameUnit
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.CHARACTER_TAG))
        {
            Character character = Cache.GetCharacter(other);
            if (character != null)
            {
                character.ChestBoost();
            }
            SimplePool.Despawn(this);
            LevelManager.Instance.chestUltiCounter.Remove(this);
        }
    }
}
