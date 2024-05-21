using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{

    public ParticleSystem particles;
    private Animator animator;
    private bool isCollected;

    void Start()
    {
        animator = GetComponent<Animator>();
        isCollected = false;
    }

    //������� ���
    public void GemCollected()
    {
        if (!isCollected) 
        {
            particles.Play();
            SoundController.instance.PlayGemPopUpSound();
            animator.SetBool("Collected", true);
            isCollected = true;
            GameController.instance.PlusScore();
        }
    }
}
