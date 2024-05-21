using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class DrawController : MonoBehaviour
{

    [Header("Line Settings")]
    public UILineRenderer uiLine;
    private Vector2[] linePoints;
    private float minDistance = 0.1f;
    private Vector3 previousMousePos;
    public Image pointer;
    public RectTransform drawPanel;

    private Vector2[] finishPosPoints;

    public static DrawController instance;
    private RunnersController runnersController;
    private GameController gameController;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        runnersController = RunnersController.instance;
        gameController = GameController.instance;

        linePoints = new Vector2[0];

        SaveFinishPoints();
        ResetLine();

        previousMousePos = Vector3.zero;
    }


    void Update()
    {
        //по нажатию рисуем
        if (Input.GetMouseButton(0))
        {
            //если нажато на панель для рисования
            if (IsMouseOverPanel())
            {

                Vector2 pointerPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(drawPanel, Input.mousePosition, null, out pointerPos);
                pointer.transform.position = pointerPos;
                Vector3 currentMousePos = pointer.transform.position;

                if (Vector3.Distance(currentMousePos, previousMousePos) >= minDistance)
                {
                    //добавляем точку
                    AddPoint(currentMousePos);

                    previousMousePos = currentMousePos;
                }
            }
        }
        //отжали кнопку, стираем рисунок и ставим бегунов
        if (Input.GetMouseButtonUp(0))
        {
            //ставим юегунов по нужной линии
            gameController.StartGame();
            runnersController.UpdateRunnersPosition(linePoints);
            ResetLine();
        }
    }


    //добавляем точку в линию
    private void AddPoint(Vector3 mousePos)
    {
        Vector2 newPoint = new Vector2(mousePos.x, mousePos.y);
        //копируем имеющийся массив точек
        Vector2[] oldpoints = new Vector2[linePoints.Length];
        for (int i = 0;i<linePoints.Length;i++)
        {
            oldpoints[i] = linePoints[i];
        }
        //переопредленяем массив
        linePoints = new Vector2[oldpoints.Length + 1];
        for (int i = 0;i< oldpoints.Length;i++)
        {
            linePoints[i] = oldpoints[i];
        }
        //добавляем точку
        linePoints[linePoints.Length - 1] = newPoint;

        uiLine.Points = linePoints;
    }

    private void SaveFinishPoints()
    {
        finishPosPoints = new Vector2[uiLine.Points.Length];
        finishPosPoints = uiLine.Points;
    }

    public void SetFinishPosition()
    {
        runnersController.UpdateRunnersPosition(finishPosPoints);
    }

    //обнуляем точки
    private void ResetLine()
    {
        linePoints = new Vector2[0];
        uiLine.Points = linePoints;
    }

    //находится ли указатель над нужной панелью
    private bool IsMouseOverPanel()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++) 
        {
            if (raycastResultsList[i].gameObject.tag != "DrawPanel")
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultsList.Count > 0;
    }
}
