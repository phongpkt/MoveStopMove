using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ranking;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text killerName;
    [SerializeField] private Player player;
    private void OnEnable()
    {
        //killerName.SetText(player.currentAttacker.characterName.ToString());
        ranking.SetText((LevelManager.Instance.enemyCounter.Count + 1).ToString() + "/" + (LevelManager.Instance.totalBotAmount + 1).ToString());
        gold.SetText("Get: " + GameManager.Instance.goldPerStage.ToString());
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
