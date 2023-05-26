using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hat", menuName = "Hats")]
public class Hat : ScriptableObject
{
    public int index;
    public Sprite icon;
    public GameObject model;
}
