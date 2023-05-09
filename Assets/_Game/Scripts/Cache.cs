using UnityEngine;
using System.Collections.Generic;


public class Cache
{

    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if(!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------


    //private static Dictionary<Collider, Burger> m_Burger = new Dictionary<Collider, Burger>();

    //public static Burger GetBurger(Collider key)
    //{
    //    if (!m_Burger.ContainsKey(key))
    //    {
    //        Burger burger = key.GetComponent<Burger>();

    //        if (burger != null)
    //        {
    //            m_Burger.Add(key, burger);
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    return m_Burger[key];
    //}


}

