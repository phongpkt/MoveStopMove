using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnim : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 1000);
    }
}
