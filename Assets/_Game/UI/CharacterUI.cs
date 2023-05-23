using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    //public Text text;
    public GameObject characterUI;
    public GameObject enemyIcon;
    public Transform target;

    private Vector3 offset = new Vector3(0, 4, 0);

    private Transform characterTf;
    private Transform enemyTf;


    private void Awake()
    {
        characterTf = characterUI.transform;
        enemyTf = enemyIcon.transform;
    }

    // Update is called once per frame
    private void Update()
    {

        float minX = 60;
        float maxX = Screen.width - minX;

        float minY = 28;
        float maxY = Screen.height - minY;

        Vector3 CharacterPos = target.position + offset;
        Vector3 EnemyPos = target.position + offset;

        Vector2 _CharacterPos = Camera.main.WorldToScreenPoint(CharacterPos);
        Vector2 enemyPos = Camera.main.WorldToScreenPoint(EnemyPos);

        enemyPos.x = Mathf.Clamp(enemyPos.x, minX, maxX);
        enemyPos.y = Mathf.Clamp(enemyPos.y, minY, maxY);

        enemyTf.position = enemyPos;
        characterTf.position = _CharacterPos;
    }
}
