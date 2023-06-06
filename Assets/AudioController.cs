using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Character player;

    //GamePlay
    [SerializeField] private AudioSource dieSource;
    [SerializeField] private AudioSource loseSource;
    [SerializeField] private AudioSource winSource;
    [SerializeField] private AudioSource attackSource;

    //UI
    [SerializeField] private AudioSource btn_ClickSource;

    public void PlayWhenAttack()
    {
        attackSource.Play();
    }
    public void PLayWhenHit()
    {
        dieSource.Play();
    }
    public void PLayWhenLose()
    {
        loseSource.Play();
    }
    public void PlayWhenWin()
    {
        winSource.Play();
    }
    public void PlayWhenClick()
    {
        btn_ClickSource.Play();
    }

}
