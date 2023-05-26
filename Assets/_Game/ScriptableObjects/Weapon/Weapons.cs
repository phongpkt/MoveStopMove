using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class Weapons : ScriptableObject
{
    public int index;
    public new string name;
    public GameObject model;
    public Sprite icon;

    public int price;
    public int range;
    public int speed;
}
