using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
[System.Serializable]
public class shapeData : ScriptableObject
{
    [System.Serializable]
    public class Row
    {
        // A row is comprised of columns
        public bool[] column;
        private int _size = 0;
        public Row() { }
        public Row(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            // A row contains n columns that are n booleans. All are set to false upon instantiation.
            _size = size;
            column = new bool[_size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                column[i] = false;
            }
        }
    }

    // The following three fields comprise the "Shape Data": 
    // Firstly, the numbers of rows and columns
    // Then, a board of Rows. As Rows contain the columns, a group of Rows make up the "Shape Board".
    // Each Row in the board is cleared upon instantiation.

    public int columns = 0;
    public int rows = 0;
    public Row[] board;
    public void Clear()
    {
        for (var i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }

    // Create a board by creating n rows of m columns.
    public void CreateNewBoard()
    {
        board = new Row[rows];
        for (var i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}