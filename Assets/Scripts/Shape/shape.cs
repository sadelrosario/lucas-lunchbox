using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.IK;
using UnityEngine.UIElements;

public class shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;
    public Vector2 offset = new Vector2(0f, 700f);

    [HideInInspector]
    public shapeData currentShapeData;

    private List<GameObject> _currentShape = new List<GameObject>();
    private Vector3 _shapeStartScale;
    private RectTransform _transform;
    private bool _shapeDraggable = true;
    private Canvas _canvas;

    public void Awake()
    {
        _shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _transform = this.GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _shapeDraggable = true;
    }

    public void RequestNewShape(shapeData sd)
    {
        CreateShape(sd);
    }

    public void CreateShape(shapeData sd)
    {
        currentShapeData = sd;
        var totalSquareNumber = GetNumberOfSquares(sd);

        while (_currentShape.Count <= totalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        for (var row = 0; row < sd.rows; row++)
        {
            for (var col = 0; col < sd.columns; col++)
            {
                if (sd.board[row].column[col])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = new Vector2(GetXPosShapeSquare(sd, col, moveDistance), GetYPosShapeSquare(sd, row, moveDistance));
                    currentIndexInList++;
                }
            }
        }
    }

    private float GetYPosShapeSquare(shapeData sd, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (sd.rows > 1)
        {
            if (sd.rows % 2 != 0)
            {
                var middleSquareIndex = (sd.rows - 1) / 2;
                var multiplier = (sd.rows - 1) / 2;
                if (row < middleSquareIndex)
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex)
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (sd.rows == 2) ? 1 : (sd.rows / 2);
                var middleSquareIndex1 = (sd.rows == 2) ? 0 : (sd.rows - 2);
                var multiplier = sd.rows / 2;

                if (row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if (row == middleSquareIndex2)
                    {
                        shiftOnY = (moveDistance.y / 2) * -1;
                    }
                    if (row == middleSquareIndex1)
                    {
                        shiftOnY = (moveDistance.y / 2);
                    }
                }

                if (row < middleSquareIndex1 && row < middleSquareIndex2)
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2)
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }

    private float GetXPosShapeSquare(shapeData sd, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if (sd.columns > 1)
        {
            if (sd.columns % 2 != 0)
            {
                var middleSquareIndex = (sd.columns - 1) / 2;
                var multiplier = (sd.columns - 1) / 2;
                if (column < middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (sd.columns == 2) ? 1 : (sd.columns / 2);
                var middleSquareIndex1 = (sd.columns == 2) ? 0 : (sd.columns - 1);
                var multiplier = sd.columns / 2;

                if (column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                    {
                        shiftOnX = moveDistance.x / 2;
                    }
                    if (column == middleSquareIndex1)
                    {
                        shiftOnX = moveDistance.x / 2 * -1;
                    }
                }

                if (column < middleSquareIndex1 && column < middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex1 && column > middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }
        return shiftOnX;
    }

    private int GetNumberOfSquares(shapeData sd)
    {
        int number = 0;

        foreach (var row in sd.board)
        {
            foreach (var active in row.column)
            {
                if (active)
                {
                    number++;
                }
            }
        }

        return number;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchorMin = new Vector2(0, 0);
        _transform.anchorMax = new Vector2(0, 0);
        _transform.pivot = new Vector2(0, 0);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, Camera.main, out pos);
        _transform.localPosition = pos + offset;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = _shapeStartScale;
        GameEvents.CheckPlaceable();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
