using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantButtonCrl : MonoBehaviour
{
    public int index;
    public SkinShopUIManager skinShop;
    public void TaskOnClick()
    {
        skinShop.ChangePlayerPant(index);
    }
}
