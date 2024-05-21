using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("Game View Settings")]
    public TMPro.TMP_Text scoreTxt;
    private bool isGameStart;
    [SerializeField]
    [Tooltip("���������� ������������ �� 1 ���")]
    private int gemValue;
    private int currentScore;

    [Header("Start View Settings")]
    public GameObject StartView;

    [Header("Defeat View Settings")]
    public GameObject defeatView;
    public Button defeatRestartBtn;

    [Header("Victory View Settings")]
    public GameObject VictoryView;
    private bool isVictory;
    public Button victoryRestartBtn;

    private RunnersController runnersController;
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        runnersController = RunnersController.instance;

        currentScore = 0;
        scoreTxt.text = $"{currentScore}";

        ButtonSettings();
        StartSettings();
    }

    private void ButtonSettings()
    {
        defeatRestartBtn.onClick.AddListener(Restart);
        victoryRestartBtn.onClick.AddListener(Restart);
    }

    //��������� ��������� �����
    private void StartSettings()
    {
        isGameStart = false;
        isVictory = false;
        defeatView.SetActive(false);
        VictoryView.SetActive(false);
        StartView.SetActive(true);
    }

    public void StartGame()
    {
        if (!isGameStart)
        {
            GameStartSettings();
            isGameStart = true;
        }
    }

    //���� ���������
    public void Defeat()
    {
        runnersController.SetFollow(false);
        defeatView.SetActive(true);
    }

    public IEnumerator Finish()
    {
        if (!isVictory) 
        {
            yield return new WaitForSeconds(0.75f);
            runnersController.SetFollow(false);

            DrawController.instance.SetFinishPosition();
            runnersController.SetAnimation("Finish", true);
            VictoryView.SetActive(true);
            isVictory = true;
        }
    }

    //������ ����
    private void GameStartSettings()
    {
        StartView.SetActive(false);

        runnersController.SetFollow(true);
    }

    //���������� ������
    public void PlusScore()
    {
        currentScore += gemValue;
        scoreTxt.text = $"{currentScore}";
        scoreTxt.gameObject.GetComponent<Animation>().Play();
    }

    //�������� ������� ������
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
