using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPrefScript : MonoBehaviour
{

    [Header("Main Settings")]
    [SerializeField]
    [Tooltip("Скорость бегуна при перемещении на новую позицию")]
    private float runnerSwapSpeed;
    private float minDist = 0.05f;
    private int runnerInd;
    public GameObject runnersContainer;

    [Header("Material Settings")]
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material defaultColor;
    public Material freeRunnerColor;
    private Material[] mats;

    [Header("Particle Settings")]
    public ParticleSystem particles;

    //[Header("Audio Settings")]
    //public AudioSource runnerPopUpSound;
    //public AudioSource runnerPopSound;

    private Vector3 targetPos;
    private Vector3 positionOffset;

    private RunnersController runnersController;

    void Start()
    {
        mats = skinnedMeshRenderer.materials;
        DefineMaterial();
        runnersController = RunnersController.instance;
        targetPos = transform.localPosition;
    }

    
    void Update()
    {
        MoveToPos();
    }

    //двигаем к цели
    private void MoveToPos()
    {
        if (Vector3.Distance(transform.localPosition, targetPos) > minDist)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, runnerSwapSpeed * Time.deltaTime);
        }
    }

    //определяем материал игрока в зависимости от его сотояния, бежит или ждет
    public void DefineMaterial()
    {
        if (gameObject.transform.parent == null)
        {
            mats[0] = freeRunnerColor;
        }
        else
        {
            mats[0] = defaultColor;
        }
        skinnedMeshRenderer.materials = mats;
    }

    public void SetNewTargetPos(Vector3 position)
    {
        targetPos = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "runner" && other.gameObject.transform.parent == null)
        {
            //добавляем бегуна
            runnersController.AddRunner(other.gameObject);
        }
        if (other.gameObject.tag == "Spike")
        {
            SoundController.instance.PlaySpikeSound();
            //убираем бегуна
            StartCoroutine(RemoveRunner());
        }
        if (other.gameObject.tag == "CircularSaw")
        {
            SoundController.instance.PlaySawSound();
            //убираем бегуна
            StartCoroutine(RemoveRunner());
        }
        if (other.gameObject.tag == "Finish")
        {
            //победа
            StartCoroutine(GameController.instance.Finish());
            other.gameObject.GetComponent<FinishController>().IsFinished();
        }
        if (other.gameObject.tag == "Gem")
        {
            other.gameObject.GetComponent<GemController>().GemCollected();
        }
    }

    //убираем бегуна
    private IEnumerator RemoveRunner()
    {
        if (gameObject.activeSelf) 
        {
            runnersController.RemoveRunner(gameObject);
            PlayParticles();
            //runnerPopSound.Play();
            SoundController.instance.PlayRunnerPopSound();
            yield return new WaitForSeconds(0.3f);
            gameObject.SetActive(false);
        }
    }

    //играем частицы
    public void PlayParticles()
    {
        particles.Play();
    }

    //играем звук подбора персонажа
    public void PlayPopUpSound()
    {
        //runnerPopUpSound.Play();
    }

    //устанавливаем индекс бегуну
    public void SetRunnerInd(int ind)
    {
        runnerInd = ind;
    }

}
