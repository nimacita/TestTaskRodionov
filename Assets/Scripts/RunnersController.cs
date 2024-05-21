using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnersController : MonoBehaviour
{

    [Header("Main Settings")]
    public GameObject runnersContainer;
    private SplineFollower runnersFollower;
    [SerializeField]
    private List<GameObject> runners;

    [Header("Runners Positions Settings")]
    [Tooltip("Отношение позиции точки lineRenderer к позиции бегуна")]
    [SerializeField]
    private int positionRatio = 88;


    private GameController gameController;
    public static RunnersController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameController = GameController.instance;
        runnersFollower = runnersContainer.GetComponent<SplineFollower>();   
        SetFollow(false);
        DefineRunners();
    }

    //меняем позицию бегунов
    public void UpdateRunnersPosition(Vector2[] points)
    {
        //если точек больше чем бегунов
        if (points.Length > runners.Count)
        {
            int step = points.Length / runners.Count;
            for (int i = 0; i < runners.Count; i++) 
            {
                MoveRunnerToNewPos(runners[i], points[i * step]);
            }
        }
        else
        {
            //если точек меньше чем бегунов
            int pointInd = 0;
            for (int i = 0; i < runners.Count; i++)
            {
                MoveRunnerToNewPos(runners[i], points[pointInd]);
                pointInd++;
                if (pointInd >= points.Length) 
                {
                    //проходимся по точкам заново
                    pointInd = 0;
                }
            }
        }
    }

    //двигаем бегуна к нужной позиции
    private void MoveRunnerToNewPos(GameObject runner, Vector2 point)
    {
        Vector3 newPosition = 
            new Vector3(point.x / positionRatio, runner.transform.localPosition.y, point.y / positionRatio);
        //двигаем бегуна к новой позиции
        runner.GetComponent<RunnerPrefScript>().SetNewTargetPos(newPosition);
        //runner.transform.position = newPosition;
    }

    //определяем начальное количество бегунов, все включенные бегуны
    private void DefineRunners()
    {
        for (int i = 0;i<runnersContainer.transform.childCount;i++)
        {
            if (runnersContainer.transform.GetChild(i).gameObject.activeSelf)
            {
                runners.Add(runnersContainer.transform.GetChild(i).gameObject);
            }
        }
    }

    //добавляем бегуна
    public void AddRunner(GameObject runner)
    {
        if (runner.transform.parent == null) 
        {
            runner.transform.SetParent(runnersContainer.transform);
            Vector3 localPosition = runner.transform.parent.InverseTransformPoint(runner.transform.position);
            localPosition.y = runners[0].transform.localPosition.y;

            RunnerPrefScript runnerPrefScript = runner.GetComponent<RunnerPrefScript>();
            runnerPrefScript.SetNewTargetPos(localPosition);
            runnerPrefScript.DefineMaterial();
            runnerPrefScript.PlayParticles();
            SoundController.instance.PlayRunnerPopUpSound();

            runner.GetComponent<Animator>().SetBool("run", true);
            runners.Add(runner);
        }
    }

    //убираем бегуна
    public void RemoveRunner(GameObject runner)
    {
        runner.transform.SetParent(null);
        runners.Remove(runner);
        IsGameEnd();
    }

    //проверяем не проиграли ли
    private void IsGameEnd()
    {
        if (runners.Count <= 0) 
        {
            gameController.Defeat();
            return;
        }
        for (int i = 0;i<runners.Count;i++)
        {
            if (runners[i].activeSelf)
            {
                return;
            }
        }
        gameController.Defeat();
    }

    //запускаем анимацию
    public void SetAnimation(string animName, bool animState)
    {
        for (int i = 0; i < runners.Count; i++) 
        {
            runners[i].GetComponent<Animator>().SetBool(animName, animState);
        }
    }

    public void SetFollow(bool value)
    {
        if (value) SetAnimation("run", true);
        runnersFollower.follow = value;
    }

}
