using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Character;

public class WeaponShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text equippedText;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private WeaponData[] scriptableObjectWeapons;
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
        if(currentWeaponIndex > 4)
        {
            currentWeaponIndex = 4;
        }
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = 0;
        }
        switch (currentWeaponIndex)
        {
            case 0:
                ShowWeaponOnUI(currentWeaponIndex);
                buyButton.SetActive(false);
                CheckArrowState();
                equipButton.SetActive(true);
                break;
            case 1:
                ShowWeaponOnUI(currentWeaponIndex);
                CheckHammerState();
                break;
            case 2:
                ShowWeaponOnUI(currentWeaponIndex);
                CheckKnifeState();
                break;
            case 3:
                ShowWeaponOnUI(currentWeaponIndex);
                CheckCandyState();
                break;
            case 4:
                ShowWeaponOnUI(currentWeaponIndex);
                CheckBoomerangState();
                break;
        }
    }
    public void ChangeCharacterWeapon ()
    {
        switch (currentWeaponIndex)
        {
            case 0:
                player.equipedWeapon = Character.Weapon.Arrow;
                player.EquipWeapon((Weapon)currentWeaponIndex);
                PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
                PlayerPrefs.Save();
                CheckArrowState();
                break;
            case 1:
                player.equipedWeapon = Character.Weapon.Hammer;
                player.EquipWeapon((Weapon)currentWeaponIndex);
                PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
                PlayerPrefs.Save();
                CheckHammerState();
                break;
            case 2:
                player.equipedWeapon = Character.Weapon.Knife;
                player.EquipWeapon((Weapon)currentWeaponIndex);
                PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
                PlayerPrefs.Save();
                CheckKnifeState();
                break;
            case 3:
                player.equipedWeapon = Character.Weapon.Candy;
                player.EquipWeapon((Weapon)currentWeaponIndex);
                PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
                PlayerPrefs.Save();
                CheckCandyState();
                break;
            case 4:
                player.equipedWeapon = Character.Weapon.Boomerang;
                player.EquipWeapon((Weapon)currentWeaponIndex);
                PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
                PlayerPrefs.Save();
                CheckBoomerangState();
                break;
        }
    }
    public void BuyWeapon()
    {
        switch (currentWeaponIndex)
        {
            case 1:
                if (GameManager.Instance.totalPlayerGold >= scriptableObjectWeapons[1].price)
                {
                    GameManager.Instance.totalPlayerGold -= scriptableObjectWeapons[1].price;
                    PlayerPrefs.SetInt("totalPlayerGold", GameManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("hammerBuyState", 1);
                    PlayerPrefs.Save();
                    CheckHammerState();
                }
                break;
            case 2:
                if (GameManager.Instance.totalPlayerGold >= scriptableObjectWeapons[2].price)
                {
                    GameManager.Instance.totalPlayerGold -= scriptableObjectWeapons[2].price;
                    PlayerPrefs.SetInt("totalPlayerGold", GameManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("knifeBuyState", 1);
                    PlayerPrefs.Save();
                    CheckKnifeState();
                }
                break;
            case 3:
                if (GameManager.Instance.totalPlayerGold >= scriptableObjectWeapons[3].price)
                {
                    GameManager.Instance.totalPlayerGold -= scriptableObjectWeapons[3].price;
                    PlayerPrefs.SetInt("totalPlayerGold", GameManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("candyBuyState", 1);
                    PlayerPrefs.Save();
                    CheckCandyState();
                }
                break;
            case 4:
                if (GameManager.Instance.totalPlayerGold >= scriptableObjectWeapons[4].price)
                {
                    GameManager.Instance.totalPlayerGold -= scriptableObjectWeapons[4].price;
                    PlayerPrefs.SetInt("totalPlayerGold", GameManager.Instance.totalPlayerGold);
                    PlayerPrefs.SetInt("boomerangBuyState", 1);
                    PlayerPrefs.Save();
                    CheckBoomerangState();
                }
                break;
        }
    }
    private void CheckArrowState()
    {
        if (player.equipedWeapon == Character.Weapon.Arrow)
        {
            equippedText.SetText("Equipped");
        }
        else equippedText.SetText("Select");
    }
    private void CheckHammerState()
    {
        int buyState = PlayerPrefs.GetInt("hammerBuyState", 0);
        switch (buyState)
        {
            case 0:
                priceText.text = scriptableObjectWeapons[1].price.ToString();
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break; 
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                if (player.equipedWeapon == Character.Weapon.Hammer)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }
    private void CheckKnifeState()
    {
        int buyState = PlayerPrefs.GetInt("knifeBuyState", 0);
        switch (buyState)
        {
            case 0:
                priceText.text = scriptableObjectWeapons[2].price.ToString();
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                if (player.equipedWeapon == Character.Weapon.Knife)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }
    private void CheckCandyState()
    {
        int buyState = PlayerPrefs.GetInt("candyBuyState", 0);
        switch (buyState)
        {
            case 0:
                priceText.text = scriptableObjectWeapons[3].price.ToString();
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                if (player.equipedWeapon == Character.Weapon.Candy)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }
    private void CheckBoomerangState()
    {
        int buyState = PlayerPrefs.GetInt("boomerangBuyState", 0);
        switch (buyState)
        {
            case 0:
                priceText.text = scriptableObjectWeapons[4].price.ToString();
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                break;
            case 1:
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                if (player.equipedWeapon == Character.Weapon.Boomerang)
                {
                    equippedText.SetText("Equipped");
                }
                else equippedText.SetText("Select");
                break;
        }
    }
    private void ShowWeaponOnUI(int _index)
    {
        if (weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapons(scriptableObjectWeapons[_index]);
        }
    }

}
