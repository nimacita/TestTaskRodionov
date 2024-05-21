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

    //������ ����� ������ ������
    public void PlayRunnerPopSound()
    {
        runnerPopSound.Play();
    }

    //������ ����� ������� ������
    public void PlayRunnerPopUpSound()
    {
        runnerPopUpSound.Play();
    }

    //������ ����� ������� ����
    public void PlayGemPopUpSound()
    {
        gemPopUpSound.Play();
    }

    //������ ����� ����
    public void PlaySawSound()
    {
        sawSound.Play();
    }

    //������ ����� ������
    public void PlayVictorySound()
    {
        victorySound.Play();
    }

    //������ ����� ����
    public void PlaySpikeSound()
    {
        spikeSound.Play();
    }

}
