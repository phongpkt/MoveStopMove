using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private TMP_Text aliveText;

    // Update is called once per frame
    void Update()
    {
        playerName.SetText(player.characterName);
        pointText.SetText(player.point.ToString());
        aliveText.SetText("Alive: " + (LevelManager.Instance.enemyCounter.Count + 1).ToString() + "/" + (LevelManager.Instance.totalBotAmount + 1).ToString());

        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            gameObject.SetActive(false);
        }
    }
}
