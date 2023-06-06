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
    private void OnDisable()
    {
        _joystick.SetActive(true);
    }
    public void ContinueGame()
    {
        gameObject.SetActive(false);
        inGameUI.SetActive(true);
    }
    public void ReturnToHome()
    {
        inGameUI.SetActive(false);
        LevelManager.Instance.OnRetry();
        this.gameObject.SetActive(false);
    }
}
