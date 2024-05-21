using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{

    [Header("Main Settings")]
    public GameObject finishImg;
    public ParticleSystem particles;
    private bool isFinished;

    void Start()
    {
        isFinished = false;
        finishImg.SetActive(true);
    }

    //прошли финишную черту
    public void IsFinished()
    {
        if (!isFinished) 
        {
            SoundController.instance.PlayVictorySound();
            finishImg.SetActive(false);
            particles.Play();
            isFinished = true;
        }
    }
}
