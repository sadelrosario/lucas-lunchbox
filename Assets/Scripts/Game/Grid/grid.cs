using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    private void OnEnable()
    {
        GameEvents.CheckPlaceable += CheckPlaceable;
    }

    private void OnDisable()
    {
        GameEvents.CheckPlaceable -= CheckPlaceable;
    }
    void Start()
    {
        CreateGrid();
    }


    // Update is called once per frame
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPositions();
    }

    private void SpawnGridSquares()
    {
        // 0 1 2 3 4 
        // 5 6 7 8 9 

        int squareIndex = 0;

        for (var row = 0; row < rows; ++row)
        {
            for (var column = 0; column < columns; ++column)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<gridSquare>().SetImage(squareIndex % 2 == 0);
                squareIndex++;
            }
        }
    }

    private void SetGridSquaresPositions()
    {
        int columnNumber = 0;
        int rowNumber = 0;
        Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + everySquareOffset;
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if (columnNumber + 1 > columns)
            {
                squareGapNumber.x = 0;
                columnNumber = 0;
                rowNumber++;
                rowMoved = false;
            }

            var pos_x_offset = _offset.x * columnNumber + (squareGapNumber.x * squaresGap);
            var pos_y_offset = _offset.y * rowNumber + (squareGapNumber.y * squaresGap);

            if (columnNumber == 0 && columnNumber % 3 == 0)
            {
                squareGapNumber.x++;
                pos_x_offset += squaresGap;
            }

            if (rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false)
            {
                rowMoved = true;
                squareGapNumber.y++;
                pos_y_offset += squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset, 0.0f);

            columnNumber++;
        }
    }

    private void CheckPlaceable()
    {
        foreach (var sq in _gridSquares)
        {
            var gridSquare = sq.GetComponent<gridSquare>();
            if (gridSquare.CheckUsable())
            {
                gridSquare.ActivateSquare();
            }
        }
    }
}
