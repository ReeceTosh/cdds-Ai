using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private const int MoveStraightCost = 10;
    private const int MoveDiagonalCost = 14;

    private GridSquare<PathNodeAI> grid;
    private List<PathNodeAI> openList;
    private List<PathNodeAI> closedList;

    //check if code breaks/doesn't function
    private PathNodeAI currentNode;

    public static AStar Instance { get; private set; }

    public AStar(int width, int height)
    {
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
        startNode.hCost = CaluclateDistance(startNode, endNode);
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

                int tentativeGCost = currentNode.gCost + CaluclateDistance(currentNode, neighbourNode);
                int fScore = tentativeGCost + 1;
                if (!openList.Contains(neighbourNode))
                {
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.fCost = fScore;
                    neighbourNode.cameFromNode = currentNode;
                    openList.Add(neighbourNode);
                }
                else if (fScore > neighbourNode.fCost)
                {
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.fCost = fScore;
                    neighbourNode.cameFromNode = currentNode;
                }
                //DebugAStarVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
            }
        }

        return CalculatePath(endNode);
    }

    //Calcluate total distance
    private int CaluclateDistance(PathNodeAI a, PathNodeAI b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
    }

    //Read
    private List<PathNodeAI> GetNeighbourList(PathNodeAI currentNode)
    {
        List<PathNodeAI> neighbourList = new List<PathNodeAI>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

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