using PixelPlay.OffScreenIndicator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharacterUI : MonoBehaviour
{
    [Range(0.5f, 0.9f)]
    [SerializeField] private float screenBoundOffset = 0.9f;
    //public Text text;
    public GameObject characterUI;
    public GameObject enemyIcon;
    public GameObject arrowIcon;

    public Transform target;
    public Vector3 offset;

    private Transform characterTf;
    private Transform enemyTf;
    private Transform arrowTf;

    private Camera mainCamera;

    [SerializeField] private Vector3 screenPosition;
    private Vector3 screenCentre;
    private Vector3 screenBounds;

    private bool isTargetOnCam;

    private void Awake()
    {
        characterTf = characterUI.transform;
        enemyTf = enemyIcon.transform;
        arrowTf = arrowIcon.transform;
        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
        arrowTf.SetParent(enemyTf, false);
    }
    private void Update()
    {
        screenPosition = OffScreenIndicatorCore.GetScreenPosition(mainCamera, target.transform.position);
        isTargetOnCam = OffScreenIndicatorCore.IsTargetVisible(screenPosition);


        if (!isTargetOnCam)
        {
            enemyIcon.SetActive(true);
            arrowIcon.SetActive(true);
            DrawIndicator();
            ArrowInicator();
        }
        else
        {
            enemyIcon.SetActive(false);
            arrowIcon.SetActive(false);
        }
        Vector3 CharacterPos = target.position + offset;
        Vector3 _characterPos = mainCamera.WorldToScreenPoint(CharacterPos);
        characterTf.position = _characterPos;
    }
    private void DrawIndicator()
    {

        float minX = enemyTf.localScale.x + 40f;
        float minY = enemyTf.localScale.y;

        float maxX = Screen.width - minX;
        float maxY = Screen.height - minY;

        Vector3 EnemyPos = target.position + offset;
        Vector3 _enemyPos = mainCamera.WorldToScreenPoint(EnemyPos);

        if (_enemyPos.z < 0)
        {
            _enemyPos.y = Screen.height - _enemyPos.y;
            _enemyPos.x = Screen.width - _enemyPos.x;
        }

        _enemyPos.x = Mathf.Clamp(_enemyPos.x, minX, maxX);
        _enemyPos.y = Mathf.Clamp(_enemyPos.y, minY, maxY);
        _enemyPos.z = 0;

        enemyTf.position = _enemyPos;
    }
    private void ArrowInicator()
    {
        float angle = float.MinValue;
        OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds * 0.2f);
        arrowTf.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        arrowTf.position = screenPosition; //Sets the position of the indicator on the screen.

    }
}
