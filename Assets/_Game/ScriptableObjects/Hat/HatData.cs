using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hat", menuName = "Hats")]
public class HatData : ScriptableObject
{
    public int index;
    public int price;
    public Sprite icon;
    public GameObject model;
}
