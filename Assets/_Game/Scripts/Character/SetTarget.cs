using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    private GameObject target;
    private List<GameObject> setTarget = new List<GameObject>();
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            target = col.GetComponent<GameObject>();
            setTarget.Add(target);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            target = null;
        }
    }
}
