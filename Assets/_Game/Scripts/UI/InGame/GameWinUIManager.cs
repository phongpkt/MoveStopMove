using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gold;
    // Start is called before the first frame update
    private void OnEnable()
    {
        gold.SetText("Get: " + GameManager.Instance.goldPerStage.ToString());
    }

    public void ReturnToMenu()
    {
        LevelManager.Instance.OnRetry();
        this.gameObject.SetActive(false);
    }
}
