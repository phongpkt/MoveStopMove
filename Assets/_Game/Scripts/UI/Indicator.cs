using PixelPlay.OffScreenIndicator;
using System.Drawing;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : GameUnit
{
    public Character target;
    public Transform targetTf;

    [SerializeField] private GameObject enemyIcon;
    [SerializeField] private GameObject arrowIcon;
    [SerializeField] private GameObject characterInfo;

    private Transform characterTf;
    private Transform enemyTf;
    private Transform arrowTf;

    private Vector3 screenCentre;
    private Vector3 offset = new Vector3(0, 4, 0);

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        enemyTf = enemyIcon.transform;
        arrowTf = arrowIcon.transform;
        characterTf = characterInfo.transform;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
    }
    public override void OnInit()
    {
        //enemyIcon.color = color;
        //arrowIcon.color = color;
    }

    public void OnInit(Character character, Transform characterTf)
    {
        target = character; 
        targetTf = characterTf;
    }

    private void Update()
    {
        Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(mainCamera, targetTf.position);
        bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);

        if(isTargetVisible)
        {
            enemyIcon.SetActive(false);
            arrowIcon.SetActive(false);
        }
        else
        {
            enemyIcon.SetActive(true);
            arrowIcon.SetActive(true);
            MoveArrow();
            MoveIcon();

        }

        Vector3 CharacterPos = targetTf.position + offset;
        Vector3 _characterPos = mainCamera.WorldToScreenPoint(CharacterPos);
        characterTf.position = _characterPos;
    }
    public void MoveArrow()
    {
        float minX = arrowTf.localScale.x / 2 + 50;
        float minY = arrowTf.localScale.y / 2 + 50;

        float maxX = Screen.width - minX - 50;
        float maxY = Screen.height - minY - 50;

        Vector3 pos = mainCamera.WorldToScreenPoint(targetTf.position + offset);

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;

        arrowTf.position = pos;

        Vector2 direction = new Vector2(pos.x - screenCentre.x, pos.y - screenCentre.y);
        float angle = Vector3.Angle(Vector2.up, direction);
        Vector3 cross = Vector3.Cross(Vector2.up, direction);
        
        if (cross.z < 0) angle = -angle;

        arrowTf.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void MoveIcon()
    {
        float minX = enemyTf.localScale.x / 2 + 100;
        float minY = enemyTf.localScale.y / 2 + 80;

        float maxX = Screen.width - minX - 80;
        float maxY = Screen.height - minY - 100;

        Vector3 pos = mainCamera.WorldToScreenPoint(targetTf.position + offset);

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;

        enemyTf.position = pos;
    }
}