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
    [Tooltip("��������� ������� ����� lineRenderer � ������� ������")]
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

    //������ ������� �������
    public void UpdateRunnersPosition(Vector2[] points)
    {
        //���� ����� ������ ��� �������
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
            //���� ����� ������ ��� �������
            int pointInd = 0;
            for (int i = 0; i < runners.Count; i++)
            {
                MoveRunnerToNewPos(runners[i], points[pointInd]);
                pointInd++;
                if (pointInd >= points.Length) 
                {
                    //���������� �� ������ ������
                    pointInd = 0;
                }
            }
        }
    }

    //������� ������ � ������ �������
    private void MoveRunnerToNewPos(GameObject runner, Vector2 point)
    {
        Vector3 newPosition = 
            new Vector3(point.x / positionRatio, runner.transform.localPosition.y, point.y / positionRatio);
        //������� ������ � ����� �������
        runner.GetComponent<RunnerPrefScript>().SetNewTargetPos(newPosition);
        //runner.transform.position = newPosition;
    }

    //���������� ��������� ���������� �������, ��� ���������� ������
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

    //��������� ������
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

    //������� ������
    public void RemoveRunner(GameObject runner)
    {
        runner.transform.SetParent(null);
        runners.Remove(runner);
        IsGameEnd();
    }

    //��������� �� ��������� ��
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

    //��������� ��������
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
