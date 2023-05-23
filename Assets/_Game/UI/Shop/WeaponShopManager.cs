using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponShopManager : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private ScriptableObject[] scriptableObjectWeapons;
    [SerializeField] private WeaponDisplay weaponDisplay;
    private int currentWeaponIndex;

    [SerializeField] private Player player;
    [SerializeField] private GameObject playerName;

    private void OnEnable()
    {
        playerName.SetActive(false);
        equipButton.SetActive(true);
        ChangeWeaponUI(0);
    }
    private void OnDisable()
    {
        playerName.SetActive(true);
    }
    public void CloseWeaponShop()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ChangeWeaponUI(int _index)
    {
        currentWeaponIndex += _index;
        if(currentWeaponIndex < 0) 
        {
            currentWeaponIndex = scriptableObjectWeapons.Length - 1;
        }
        if(currentWeaponIndex > scriptableObjectWeapons.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        if(weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapons((Weapon)scriptableObjectWeapons[currentWeaponIndex]);
        }
    }
    public void ChangeCharacterWeapon ()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                player.equipedWeapon = Character.Weapon.Arrow;
                player.EquipWeapon();
                PlayerPrefs.Save();

                break;
            case 1:
                player.equipedWeapon = Character.Weapon.Hammer;
                player.EquipWeapon();
                PlayerPrefs.Save();

                break;
            case 2:
                player.equipedWeapon = Character.Weapon.Knife;
                player.EquipWeapon();
                PlayerPrefs.Save();

                break;
            case 3:
                player.equipedWeapon = Character.Weapon.Candy;
                player.EquipWeapon();
                PlayerPrefs.Save();

                break;
            case 4:
                player.equipedWeapon = Character.Weapon.Boomerang;
                player.EquipWeapon();
                PlayerPrefs.Save();

                break;
        }
    }
}
