using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePauseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject _joystick;
    private void OnEnable()
    {
        _joystick.SetActive(false);
    }
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        inGameUI.SetActive(true);
        _joystick.SetActive(true);
    }
    public void ReturnToHome()
    {
        inGameUI.SetActive(false);
        this.gameObject.SetActive(false);
        LevelManager.Instance.OnRetry();
    }
}
