using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    [Header("Sound Settings")]
    public AudioSource runnerPopSound;
    public AudioSource runnerPopUpSound;
    public AudioSource gemPopUpSound;
    public AudioSource sawSound;
    public AudioSource victorySound;
    public AudioSource spikeSound;
    

    public static SoundController instance;

    private void Awake()
    {
        instance = this;
    }

    //играем звука смерти бегуна
    public void PlayRunnerPopSound()
    {
        runnerPopSound.Play();
    }

    //играем звука побдора бегуна
    public void PlayRunnerPopUpSound()
    {
        runnerPopUpSound.Play();
    }

    //играем звука побдора гема
    public void PlayGemPopUpSound()
    {
        gemPopUpSound.Play();
    }

    //играем звука пилы
    public void PlaySawSound()
    {
        sawSound.Play();
    }

    //играем звука победы
    public void PlayVictorySound()
    {
        victorySound.Play();
    }

    //играем звука шипы
    public void PlaySpikeSound()
    {
        spikeSound.Play();
    }

}
