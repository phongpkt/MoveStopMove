using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponEffect;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Image weaponImage;
    public void DisplayWeapons(WeaponData _weapon)
    {
        weaponName.SetText(_weapon.name);
        weaponImage.sprite = _weapon.icon;

        if (_weapon.range > 1 && _weapon.speed <= 1)
        {
            weaponEffect.SetText("+ " + _weapon.range + " Attack Range");
        }
        else if (_weapon.range <= 1 && _weapon.speed > 1)
        {
            weaponEffect.SetText("+ " + _weapon.speed + " Attack Speed");
        }
        else
        {
            weaponEffect.SetText("+ " + _weapon.range + " Attack Range, " + _weapon.speed + " Attack Speed");
        }
    }
}
