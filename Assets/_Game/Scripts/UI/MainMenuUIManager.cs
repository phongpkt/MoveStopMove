using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponShop;
    [SerializeField]
    private GameObject skinShop;
    [SerializeField]
    private TMP_Text gold;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject _joystick;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField] private Player player;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private GameObject playerNameIPF;

    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject audioController;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject rumbleOn;
    [SerializeField] private GameObject rumbleOff;

    private void OnEnable()
    {
        _joystick.SetActive(false);
        OpenMainMenu();
        playerNameIPF.SetActive(true);
        playerName.text = PlayerPrefs.GetString("playerName", "Player");
        playerName.onEndEdit.AddListener(delegate { SaveName(); });
    }

    private void Update()
    {
        gold.SetText(GameManager.Instance.totalPlayerGold.ToString());
    }

    private void SaveName()
    {
        player.characterName = playerName.text;
        PlayerPrefs.SetString("playerName", playerName.text);
        PlayerPrefs.Save();
    }

    public void OpenMainMenu()
    {
        anim.SetBool("IsShow", true);
        anim.SetBool("IsHide", false);
        gameObject.SetActive(true);
    }

    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
        CloseMainMenu();
    }

    public void ShowSkinShop()
    {
        skinShop.SetActive(true);
        CloseMainMenu();
    }
    public void PressPlayButton()
    {
        _joystick.SetActive(true);
        GameManager.Instance.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnPlay();
        StartCoroutine(OpenInGameUI());
        CloseMainMenu();
    }
    public void TurnSoundOff()
    {
        soundOn.SetActive(false);
        audioController.SetActive(false);
        soundOff.SetActive(true);
    }
    public void TurnSoundOn()
    {
        soundOn.SetActive(true);
        audioController.SetActive(true);
        soundOff.SetActive(false);
    }
    public void TurnRumbleOff()
    {
        rumbleOn.SetActive(false);
        rumbleOff.SetActive(true);
    }
    public void TurnRumbleOn()
    {
        rumbleOn.SetActive(true);
        rumbleOff.SetActive(false);
    }
    IEnumerator OpenInGameUI()
    {
        yield return new WaitForSeconds(1);
        inGameUI.SetActive(true);
    }

    public void CloseMainMenu()
    {
        anim.SetBool("IsShow", false);
        anim.SetBool("IsHide", true);
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            playerNameIPF.SetActive(false);
            StartCoroutine(HideMainMenu());
        }
    }

    IEnumerator HideMainMenu()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
