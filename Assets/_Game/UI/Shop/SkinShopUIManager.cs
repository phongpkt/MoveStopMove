using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUIManager : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject[] pages;
    private int currentPage;

    [SerializeField] private Hat[] hats;
    [SerializeField] private Shield[] shields;
    [SerializeField] private Pant[] pants;
    [SerializeField] private Fullset[] fullsets;


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
    public void ChangePlayerHead(int _index)
    {
        switch (_index)
        {
            case 0:
                SpawnHat(_index);
                break;
            case 1:
                SpawnHat(_index);
                break;
            case 2:
                SpawnHat(_index);
                break;
            case 3:
                SpawnHat(_index);
                break;
            case 4:
                SpawnHat(_index);
                break;
            case 5:
                SpawnHat(_index);
                break;
            case 6:
                SpawnHat(_index);
                break;
            case 7:
                SpawnHat(_index);
                break;
            case 8:
                SpawnHat(_index);
                break;
            case 9:
                SpawnHat(_index);
                break;
        }
    }
    private void SpawnHat(int index)
    {
        Transform hat = player.headPosition;
        if (hat.childCount > 0)
        {
            Destroy(hat.GetChild(0).gameObject);
        }
        foreach (var item in hats)
        {
            if (item != null && item.index == index)
            {
                Instantiate(item.model, player.headPosition);
            }
        }
    }
    public void ChangePlayerPant(int _index)
    {
        switch (_index)
        {
            case 0:
                ChangePants(_index);
                break;
            case 1:
                ChangePants(_index);
                break;
            case 2:
                ChangePants(_index);
                break;
            case 3:
                ChangePants(_index);
                break;
            case 4:
                ChangePants(_index);
                break;
            case 5:
                ChangePants(_index);
                break;
            case 6:
                ChangePants(_index);
                break;
            case 7:
                ChangePants(_index);
                break;
            case 8:
                ChangePants(_index);
                break;
            default:
                break;
        }
    }
    private void ChangePants(int index)
    {
        foreach (var item in pants)
        {
            if (item != null && item.index == index)
            {
                player.pantRenderer.material = item.material;
            }
        }
    }
    public void ChangePlayerShield(int _index)
    {
        switch (_index)
        {
            case 0:
                ChangeShield(_index);
                break;
            case 1:
                ChangeShield(_index);
                break;
        }
    }
    private void ChangeShield(int index)
    {
        Transform shield = player.shieldPosition;
        if (shield.childCount > 0)
        {
            Destroy(shield.GetChild(0).gameObject);
        }
        foreach (var item in shields)
        {
            if (item != null && item.index == index)
            {
                Instantiate(item.model, player.shieldPosition);
            }
        }
    }
    public void ChangePlayerSkin(int _index)
    {
        switch (_index)
        {
            case 0:
                ChangeSkin(_index);
                break;
            case 1:
                ChangeSkin(_index);
                break;
            case 2:
                ChangeSkin(_index);
                break;
            case 3:
                ChangeSkin(_index);
                break;
            case 4:
                ChangeSkin(_index);
                break;
        }
    }
    private void ChangeSkin(int index)
    {
        ResetSkin();
        foreach (var item in fullsets)
        {
            if (item.index == index)
            {
                if(item.Head != null)
                {
                    Instantiate(item.Head, player.headPosition);
                }
                if (item.Tail != null)
                {
                    Instantiate(item.Tail, player.tailPosition);
                }
                if (item.Back != null)
                {
                    Instantiate(item.Back, player.backPosition);
                }
                if (item.LeftHand != null)
                {
                    Instantiate(item.LeftHand, player.shieldPosition);
                }
                player.skinRenderer.material = item.Skin;
            }
        }
    }
    private void ResetSkin()
    {
        Transform hat = player.headPosition;
        Transform back = player.backPosition;
        Transform shield = player.shieldPosition;
        Transform tail = player.tailPosition;

        if (hat.childCount > 0)
        {
            Destroy(hat.GetChild(0).gameObject);
        }
        if (back.childCount > 0)
        {
            Destroy(back.GetChild(0).gameObject);
        }
        if (shield.childCount > 0)
        {
            Destroy(shield.GetChild(0).gameObject);
        }
        if (tail.childCount > 0)
        {
            Destroy(tail.GetChild(0).gameObject);
        }
    }
}
