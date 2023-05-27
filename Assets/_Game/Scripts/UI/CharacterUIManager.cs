using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pointText;
    [SerializeField]
    private TMP_Text enemyName;
    [SerializeField]
    private Bot bot;

    // Update is called once per frame
    void Update()
    {
        enemyName.SetText(bot.characterName.ToString());
        pointText.SetText(bot.point.ToString());
    }
}
