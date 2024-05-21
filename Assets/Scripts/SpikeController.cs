using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{

    [Header("Animation Settings")]
    [Tooltip("����� �� ����� ��������")]
    public int waitToNextAnim;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartHideAnimation(true));
    }

    //������ �������� �������� ����
    private IEnumerator StartHideAnimation(bool isStart = false)
    {
        if (!isStart) {
            //animations.Play("Hide");
            anim.SetBool("Hide",true);
        }
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(waitToNextAnim);
        StartCoroutine(StartAppearAnimation());
    }

    //������ �������� ����������� ����
    private IEnumerator StartAppearAnimation()
    {
        //animations.Play("Appear");
        anim.SetBool("Hide", false);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(waitToNextAnim);
        StartCoroutine (StartHideAnimation());
    }


}
