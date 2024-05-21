using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgCanvasController : MonoBehaviour
{

    [Header("Main Settings")]
    public GameObject targetCamera;
    private Vector3 offset;
    public RawImage bgImg;
    public float imgScrollX, imgScrollY;


    private void Start()
    {
        offset = transform.position;
    }


    void Update()
    {
        MoveCanvas();
        ScrollImg();
    }

    //������� �� ������� ������
    private void MoveCanvas()
    {
        transform.position = targetCamera.transform.position + offset;

        Vector3 newRot = transform.rotation.eulerAngles;
        newRot.y = targetCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(newRot);
    }

    //������� �������� ��������
    private void ScrollImg()
    {
        bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(imgScrollX, imgScrollY) * Time.deltaTime, bgImg.uvRect.size);
    }
}
