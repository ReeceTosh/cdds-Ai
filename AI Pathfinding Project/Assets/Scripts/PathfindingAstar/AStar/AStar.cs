using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    //NOTE: The algorithm plays but it isn't choosing the quickest path there, look at search algorithm, aie lecture and tutrorial as to why this may be.
    public static int squareCount = 0;

    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;

    private GridSquare<PathNodeAI> grid;
    private List<PathNodeAI> openList;
    private List<PathNodeAI> closedList;

    private PathNodeAI currentNode;

    public static AStar Instance { get; private set; }

    public AStar(int width, int height)
    {
        Instance = this;
        grid = new GridSquare<PathNodeAI>(width, height, 10f, Vector3.zero, (GridSquare<PathNodeAI> grid, int x, int y) => new PathNodeAI(grid, x, y));
    }

    public GridSquare<PathNodeAI> GetGrid()
    {
        return grid;
    }

    //Read
    public List<PathNodeAI> FindPath(int startX, int startY, int endX, int endY)
    {
        //Gets start and end node in the grid path
        PathNodeAI startNode = grid.GetGridObject(startX, startY);
        PathNodeAI endNode = grid.GetGridObject(endX, endY);

        //if it is a illegitimate path
        if (startNode == null || endNode == null)
        {
           //Debug.Log("start or end is null");
           return null;
        }

        openList = new List<PathNodeAI> { startNode };
        closedList = new List<PathNodeAI>();

        //Set up of default grid costs
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNodeAI pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFc();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFc();

        //DebugAStarVisual.Instance.ClearSnapshots();
        //DebugAStarVisual.Instance.TakeSnapshot(grid, startNode, openList, closedList);

        while (openList.Count > 0)
        {
            currentNode = GetLowestFCostNode(openList);
            if (currentNode.x == endNode.x && currentNode.y == endNode.y)
            {
                //DebugAStarVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                //DebugAStarVisual.Instance.TakeSnapshotFinalPath(grid, CalculatePath(endNode));
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNodeAI neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                int hScore = CalculateDistance(neighbourNode, endNode);
                int fScore = tentativeGCost + hScore;
                if (!openList.Contains(neighbourNode))
                {
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = hScore;
                    neighbourNode.fCost = fScore;
                    neighbourNode.cameFromNode = currentNode;
                    openList.Add(neighbourNode);
                }
                else if (fScore > neighbourNode.fCost)
                {
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = hScore;
                    neighbourNode.fCost = fScore;
                    neighbourNode.cameFromNode = currentNode;
                }
                //DebugAStarVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
            }
        }

        return CalculatePath(endNode);
    }

    //Calcluate total distance
    private int CalculateDistance(PathNodeAI a, PathNodeAI b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
    }

    //This is part of the search algorithm, this retreives all neighbouring nodes of the current node (Horizontal, Vertical and Diagonal).
    private List<PathNodeAI> GetNeighbourList(PathNodeAI currentNode)
    {
        List<PathNodeAI> neighbourList = new List<PathNodeAI>();
        Debug.Log("Grid Square #" + squareCount.ToString());

        if (currentNode.x - 1 >= 0)
        {
            // Left Down
            if (currentNode.y - 1 >= 0)
            {
                if (!GetNode(currentNode.x - 1, currentNode.y).isWalkable && !GetNode(currentNode.x, currentNode.y - 1).isWalkable)
                {
                    //Do nothing
                    Debug.Log("Left-Down node checked but isn't applicable");
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
                    Debug.Log("Left-down node checked successfully.");
                }
            }

            // Left Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                if (!GetNode(currentNode.x - 1, currentNode.y).isWalkable && !GetNode(currentNode.x, currentNode.y + 1).isWalkable)
                {
                    //Do nothing
                    Debug.Log("Left-up node checked but isn't applicable");
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
                    Debug.Log("Left-up node checked successfully.");
                }
            }

            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            
            // Right Down
            if (currentNode.y - 1 >= 0)
            {
                if (!GetNode(currentNode.x + 1, currentNode.y).isWalkable && !GetNode(currentNode.x, currentNode.y - 1).isWalkable)
                {
                    //Do nothing.
                    Debug.Log("Right-Down node checked but isn't applicable");
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                    Debug.Log("Right-down node checked successfully.");
                }
            }


            // Right Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                //Check the node isWalkable true for the AI.
                if (!GetNode(currentNode.x + 1, currentNode.y).isWalkable && !GetNode(currentNode.x, currentNode.y + 1).isWalkable)
                {
                    //Do nothing.
                    Debug.Log("Right-up node checked but isn't applicable");
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
                    Debug.Log("Right-up node checked successfully.");
                }
            }

            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        return neighbourList;
    }

    public PathNodeAI GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    //Read
    private List<PathNodeAI> CalculatePath(PathNodeAI endNode)
    {
        List<PathNodeAI> path = new List<PathNodeAI>();
        path.Add(endNode);
        PathNodeAI currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
            
        }
        path.Reverse();
        //Debug.Log("Calculating Path");

        return path;
    }

    //Read
    private PathNodeAI GetLowestFCostNode(List<PathNodeAI> pathNodeList)
    {
        PathNodeAI lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}