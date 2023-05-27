using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButtonCrl : MonoBehaviour
{
    public int index;
    public SkinShopUIManager skinShop;
    public void TaskOnClick()
    {
        skinShop.ChangePlayerSkin(index);
    }
}
