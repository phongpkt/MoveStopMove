using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    public Player player;
    public Bot enemy;
    public Queue<Bot> targets = new Queue<Bot>();
    
    //private void Update()
    //{
    //    Debug.Log(enemy);
    //}
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            enemy = col.GetComponent<Bot>();
            targets.Enqueue(enemy);
            player.isAttack = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            enemy = null;
            player.isAttack = false;
        }
    }
}
