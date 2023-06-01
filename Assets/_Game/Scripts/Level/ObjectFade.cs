using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    private Material material;
    private Color fadeColor;
    private bool isFade;
    void Start()
    {
        isFade = false;
        material = _renderer.material;
        fadeColor = material.color;
    }

    void Update()
    {
        if (isFade)
        {
            MaterialObjectFade.MakeFade(material);
            fadeColor.a = 0.5f;
        }
        else
        {
            MaterialObjectFade.MakeOpaque(material);
            fadeColor.a = 1f;
        }

        material.color = fadeColor;
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = Cache.GetPlayer(other);
        if (player != null)
        {
            isFade = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = Cache.GetPlayer(other);
        if (player != null)
        {
            isFade = false;
        }
    }
}
