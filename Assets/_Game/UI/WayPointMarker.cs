using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPointMarker : MonoBehaviour
{
    public Image image;
    public Transform target;
    public Vector2 imageSize;
    public Vector3 offset;

    private void Update()
    {
        float minX = imageSize.x / 2;
        float minY = imageSize.y / 2;

        float maxX = Screen.width - minX;
        float maxY = Screen.height - minY;
        Vector3 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;
        image.transform.position = pos;

    }
}
