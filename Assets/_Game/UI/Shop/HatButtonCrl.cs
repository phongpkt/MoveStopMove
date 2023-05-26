using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HatButtonCrl : MonoBehaviour
{
    public int index;
    public SkinShopUIManager skinShop;
    public void TaskOnClick()
    {
        skinShop.ChangePlayerHead(index);
    }
}
