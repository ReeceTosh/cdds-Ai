using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GridSquare<TGridObject>
{
    public event EventHandler<OnGridSquareObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridSquareObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public const int obstructionValueMax = 100;
    public const int obstructionValueMin = 0;

    private int height;
    private int width;
    private float cellSize;
    //private int[,] gridArray;
    protected Vector3 originPostition;
    private TGridObject[,] gridArray;

    public GridSquare(int width, int height, float cellSize, Vector3 originPostition, Func<GridSquare<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.originPostition = originPostition;
        this.cellSize = cellSize;
        this.height = height;
        this.width = width;

        gridArray = new TGridObject[width, height];
        //debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = UtilityClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);

                    //Draws'Vertical lines'
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            //Draws 'Horizontal lines'
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridSquareObjectChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };

            //Tests if the value will be set and generated within the coordinates
            //SetValue(2, 1, 56);
        }


    }

    public int GetWidth()
    {
        return (int)width;
    }

    public int GetHeight()
    {
        return (int)height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    //Finds the grid position and sets the value of on that grid equal to the value passed in parameters
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridSquareObjectChangedEventArgs { x = x, y = y });
        }
    }

    //Finds grid position and sets an image instead of a value, work in progress.
    public void SetGridObjectImage(int x, int y, Image image)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {


            //gridArray[x, y] = image;
            //if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridSquareObjectChangedEventArgs { x = x, y = y });
        }
    }

    //If the gridobject exits then set it equal to the x and y parameters passed in???
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridSquareObjectChangedEventArgs { x = x, y = y });
    }
    //Finds the world position and retrieves what the grid position is and sets value within that position to the value passed in parameters
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }
    //Finds and reads the the grid position
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }




    //Sets a value of your choice within the coordinates in world space
    //public void SetValue(int x, int y, int value)
    //{
    //    if (x >= 0 && y >= 0 && x < width && y < height)
    //    {
    //        gridArray[x, y] = Mathf.Clamp(value, obstructionValueMin, obstructionValueMax);
    //        debugTextArray[x, y].text = gridArray[x, y].ToString();
    //    }
    //}
    //public void SetValue(Vector3 worldPosition, int value)
    //{
    //    int x, y;
    //    GetXY(worldPosition, out x, out y);
    //    SetValue(x, y, value);
    //}


    //Gets a value of your choice within the coordinates in world space
    //public int GetValue(int x, int y)
    //{
    //    if (x >= 0 && y >= 0 && x < width && y < height)
    //    {
    //        return gridArray[x, y];
    //    }
    //    else
    //    {
    //        return 0;
    //    }
    //}
    //public int GetValue(Vector3 worldPosition)
    //{
    //    int x, y;
    //    GetXY(worldPosition, out x, out y);
    //    return GetValue(x, y);
    //}
    //-------------------------------------------------------------------------

    //Retrieves world postion.
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPostition;
    }


    //Retrieves the values of X and Y from world position and converts them to int divided by cellsize.
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPostition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPostition).y / cellSize);
    }

}
