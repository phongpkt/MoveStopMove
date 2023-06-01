using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set", menuName = "Fullset")]
public class FullsetData : ScriptableObject
{
    public int index;
    public Material Skin;
    public GameObject Head;
    public GameObject LeftHand;
    public GameObject Tail;
    public GameObject Back;
}
