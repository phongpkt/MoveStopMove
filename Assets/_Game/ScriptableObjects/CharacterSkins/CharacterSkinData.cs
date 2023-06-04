using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class CharacterSkinData : ScriptableObject
{
    public List<Material> skinColor;
}
