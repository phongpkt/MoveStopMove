using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinUIManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;

    [SerializeField] private TMP_Text gold;
    private void OnEnable()
    {
        inGameUI.SetActive(false);
        gold.SetText("Get: " + GameManager.Instance.goldPerStage.ToString());
    }

    public void ReturnToMenu()
    {
        LevelManager.Instance.OnRetry();
        this.gameObject.SetActive(false);
    }
}
