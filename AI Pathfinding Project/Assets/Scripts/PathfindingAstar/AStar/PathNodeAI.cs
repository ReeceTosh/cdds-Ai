using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeAI
{
    public enum nodeType
    {
        isWalkable,
        playerSpawn,
        enemySpawn
    }

    private GridSquare grid;
    public int x;
    public int y;

    //Cost variables
    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNodeAI cameFromNode;

    public PathNodeAI(GridSquare grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }

    public void CalculateFc()
    {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }
    
}
