using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class TestAstarClick : MonoBehaviour
{
    [SerializeField] private Sprite Wall;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    private AStar pathfinding;

    void Start()
    {
        pathfinding = new AStar(10, 10);

    }
    void SpawnEnemy()
    {
        int x = UnityEngine.Random.Range(0, pathfinding.GetGrid().GetWidth());
        int y = UnityEngine.Random.Range(0, pathfinding.GetGrid().GetHeight());

        Vector3 pos = pathfinding.GetGrid().GetWorldPosition(x, y);
        //pos = 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //(int)player.transform.position.x, (int)player.transform.position.y

            //Get world position within the grid and find the path from 0,0 to where ever the mouse was clicked
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNodeAI> path = pathfinding.FindPath(0, 0, x, y);
            Debug.Log("Path length:" + path.Count);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    AStar.squareCount++;
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5);
                }

            }
        }

        //Sets up grid with walkable tiles
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            if (pathfinding.GetNode(x, y) == null)
            {
                return;
            }
            else
            {
                pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
            }
        }
    }
}
