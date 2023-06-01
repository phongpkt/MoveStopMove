using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Pants")]
public class PantData : ScriptableObject
{
    public int index;
    public Sprite icon;
    public Material material;
    public int speedBuff;
    public int rangeBuff;
}
