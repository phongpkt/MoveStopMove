using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUIManager : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text equipText;

    [SerializeField] private GameObject[] pages;
    private int currentPage;

    [SerializeField] private HatData[] hats;
    [SerializeField] private ShieldData[] shields;
    [SerializeField] private PantData[] pants;
    [SerializeField] private FullsetData[] fullsets;


    [SerializeField] private GameObject playerName;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        playerName.SetActive(false);
        ChangePage(0);
    }
    private void OnDisable()
    {
        playerName.SetActive(true);
    }
    public void CloseSkinShop()
    {
        gameObject.SetActive(false);
    }
    public void ChangePage(int index)
    {
        switch (currentPage)
        {
            case 0:
                //show hat page
                ChangeOptions(index);
                break;
            case 1:
                //show pant page
                ChangeOptions(index);
                break;
            case 2:
                //show shield page
                ChangeOptions(index);
                break;
            case 3:
                //show skin page
                ChangeOptions(index);
                break;
        }
    }
    private void ChangeOptions(int _index)
    {
        pages[currentPage].SetActive(false);
        currentPage = _index;
        pages[currentPage].SetActive(true);
    }

    //Change player skin
    #region Change Player Skin
    public void ChangePlayerHead(int _index)
    {
        switch (_index)
        {
            case 0:
                player.EquipHat(Hats.rau, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save();
                break;
            case 1:
                player.EquipHat(Hats.crown, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save();
                break;
            case 2:
                player.EquipHat(Hats.cowboy, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save();
                break;
            case 3:
                player.EquipHat(Hats.ear, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save();
                break;
            case 4:
                player.EquipHat(Hats.hat, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
            case 5:
                player.EquipHat(Hats.hat_cap, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
            case 6:
                player.EquipHat(Hats.hat_yellow, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
            case 7:
                player.EquipHat(Hats.headPhone, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
            case 8:
                player.EquipHat(Hats.arrow, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
            case 9:
                player.EquipHat(Hats.horn, _index);
                PlayerPrefs.SetInt("hat", _index);
                PlayerPrefs.Save(); 
                break;
        }
    }
    public void ChangePlayerPant(int _index)
    {
        switch (_index)
        {
            case 0:
                player.EquipPant(Pants.batman, _index);
                break;
            case 1:
                player.EquipPant(Pants.chambi, _index);
                break;
            case 2:
                player.EquipPant(Pants.comy, _index);
                break;
            case 3:
                player.EquipPant(Pants.dabao, _index);
                break;
            case 4:
                player.EquipPant(Pants.onion, _index); 
                break;
            case 5:
                player.EquipPant(Pants.pokemon, _index);
                break;
            case 6:
                player.EquipPant(Pants.rainbow, _index);
                break;
            case 7:
                player.EquipPant(Pants.skull, _index);
                break;
            case 8:
                player.EquipPant(Pants.vantim, _index);
                break;
        }
    }
    public void ChangePlayerShield(int _index)
    {
        switch (_index)
        {
            case 0:
                player.EquipShield(Shields.khien, _index);
                break;
            case 1:
                player.EquipShield(Shields.shield, _index);
                break;
        }
    }
    public void ChangePlayerSkin(int _index)
    {
        switch (_index)
        {
            case 0:
                player.EquipFullset(FullSets.devil, _index);
                break;
            case 1:
                player.EquipFullset(FullSets.angel, _index);
                break;
            case 2:
                player.EquipFullset(FullSets.witch, _index);
                break;
            case 3:
                player.EquipFullset(FullSets.deadpool, _index);
                break;
            case 4:
                player.EquipFullset(FullSets.thor, _index);
                break;
        }
    }
    #endregion

    #region Buy Skin
    public void CheckHatState(int btnIndex)
    {
        int state = PlayerPrefs.GetInt("clothesState" + btnIndex, 0);
        switch (state)
        {
            case 0: //chua mua
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                priceText.SetText(hats[btnIndex].price.ToString());
                break;
            case 1: //da mua, chua equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Select");
                break;
            case 2://da equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Equipped");
                break;
        }
    }
    private void CheckPantState(int btnIndex)
    {
        int state = PlayerPrefs.GetInt("clothesState" + btnIndex, 0);
        switch (state)
        {
            case 0: //chua mua
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                //priceText.SetText(pants[btnIndex].price.ToString());
                break;
            case 1: //da mua, chua equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Select");
                break;
            case 2://da equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Equipped");
                break;
        }
    }
    private void CheckShieldState(int btnIndex)
    {
        int state = PlayerPrefs.GetInt("headState" + btnIndex, 0);
        switch (state)
        {
            case 0: //chua mua
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                //priceText.SetText(shields[btnIndex].price.ToString());
                break;
            case 1: //da mua, chua equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Select");
                break;
            case 2://da equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Equipped");
                break;
        }
    }
    private void CheckSkinState(int btnIndex)
    {
        int state = PlayerPrefs.GetInt("headState" + btnIndex, 0);
        switch (state)
        {
            case 0: //chua mua
                buyButton.SetActive(true);
                equipButton.SetActive(false);
                //priceText.SetText(skin[btnIndex].price.ToString());
                break;
            case 1: //da mua, chua equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Select");
                break;
            case 2://da equip
                buyButton.SetActive(false);
                equipButton.SetActive(true);
                equipText.SetText("Equipped");
                break;
        }
    }
    #endregion
}
