using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridSquare : MonoBehaviour
{
    public const int obstructionValueMax = 100;
    public const int obstructionValueMin = 0;

    private int height;
    private int width;
    private float cellSize;
    private int[,] gridArray;
    protected Vector3 originPostition;
    private TextMesh[,] debugTextArray;

    public GridSquare(int height, int width, float cellSize, Vector3 originPostition = new Vector3())
    {
        this.originPostition = originPostition;
        this.cellSize = cellSize;
        this.height = height;
        this.width = width;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x, y] = UtilsClass.CreateGridText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);

                //Draws'Vertical lines'
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 15f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 15f);
            }
        }
        //Draws 'Horizontal lines'
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 15f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 15f);

        //Tests if the value will be set and generated within the coordinates
        //SetValue(2, 1, 56);
    }

    public int GetWidth()
    {
        return (int)width;
    }

    public int GetHeight()
    {
        return (int)height;
    }

    //Sets a value of your choice within the coordinates in world space
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Mathf.Clamp(value, obstructionValueMin, obstructionValueMax);
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }


    //Gets a value of your choice within the coordinates in world space
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
    //-------------------------------------------------------------------------

    //Retrieves world postion.
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPostition;
    }


    //Retrieves the values of X and Y from world position and converts them to int divided by cellsize.
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPostition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPostition).y / cellSize);
    }

}
