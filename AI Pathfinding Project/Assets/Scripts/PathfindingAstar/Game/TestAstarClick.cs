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

    private int xAxisGrid = 12;
    private int yAxisGrid = 12;

    void Start()
    {
        pathfinding = new AStar(xAxisGrid, yAxisGrid, Vector3.zero);
        //pathfinding.GetNode(0, 1).SetIsWalkable(!pathfinding.GetNode(0, 1).isWalkable);
        MapGeneration();

        SpawnEnemy();
        SpawnPlayer();
    }

    void MapGeneration()
    {
        for (int x = 1; x < pathfinding.GetGrid().GetWidth() - 1; x++)
        {
            pathfinding.GetNode(x, 0).SetIsWalkable(!pathfinding.GetNode(x, 0).isWalkable);
        }

        for (int y = 1; y < pathfinding.GetGrid().GetHeight() - 1; y++)
        {
            pathfinding.GetNode(0, y).SetIsWalkable(!pathfinding.GetNode(0, y).isWalkable);
        }

        for (int y = 0; y < pathfinding.GetGrid().GetHeight() - 1; y++)
        {
            pathfinding.GetNode(xAxisGrid - 1, y).SetIsWalkable(!pathfinding.GetNode(xAxisGrid - 1, y).isWalkable);
        }

        for (int x = 0; x < pathfinding.GetGrid().GetHeight() - 1; x++)
        {
            pathfinding.GetNode(x, yAxisGrid - 1).SetIsWalkable(!pathfinding.GetNode(x, yAxisGrid - 1).isWalkable);
        }

        pathfinding.GetNode(xAxisGrid - 1, yAxisGrid - 1).SetIsWalkable(!pathfinding.GetNode(xAxisGrid - 1, yAxisGrid - 1).isWalkable);
        pathfinding.GetNode(0, 0).SetIsWalkable(!pathfinding.GetNode(0, 0).isWalkable);

        Vector2[] cords ={ new Vector2(1, 10) , new Vector2( 3, 9 ), new Vector2( 5, 9 ), new Vector2( 7, 9 ), new Vector2( 6, 8 ), new Vector2( 7, 8 ), new Vector2( 8, 8 ), new Vector2( 2, 7 ), new Vector2( 4, 7 ), new Vector2( 7, 7 ),
                         new Vector2( 2, 5 ), new Vector2( 4, 5 ), new Vector2( 6, 5 ), new Vector2( 8, 5 ), new Vector2( 2, 4 ), new Vector2( 9, 4 ), new Vector2( 2, 3 ), new Vector2( 3, 3 ), new Vector2( 5, 3 ), new Vector2( 6, 3 ), new Vector2( 8, 3 ),
                         new Vector2( 3, 2 ), new Vector2( 7, 2 ), new Vector2( 5, 1 ), new Vector2( 9, 1 ) };

        for (int i = 0; i < cords.Length; i++)
        {
            pathfinding.GetNode((int)cords[i].x, (int)cords[i].y).SetIsWalkable(!pathfinding.GetNode((int)cords[i].x, (int)cords[i].y).isWalkable);
        }

    }

    void CordGeneration(out int x, out int y)
    {
        x = UnityEngine.Random.Range(0, pathfinding.GetGrid().GetWidth());
        y = UnityEngine.Random.Range(0, pathfinding.GetGrid().GetHeight());
    }

    void SpawnEnemy()
    {


        //Update Check at some point
        int x;
        int y;
        Vector3 pos;
        CordGeneration(out x, out y);

        if (!pathfinding.GetNode(x, y).isWalkable)
        {
            CordGeneration(out x, out y);
        }
        else
        {
            pos = pathfinding.GetGrid().GetWorldPosition(x, y);
            pos.x += 5f;
            pos.y += 5f;

            GameObject clone = Instantiate(enemy);
            clone.transform.position = pos;

            pos.x -= 5f;
            pos.y -= 5f;
            Debug.Log(pos);
        }
    }

    void SpawnPlayer()
    {
        Vector3 pos = pathfinding.GetGrid().GetWorldPosition(1, 1);
        pos.x += 5f;
        pos.y += 5f;

        GameObject clone = Instantiate(player);
        clone.transform.position = pos;

        pos.x -= 5f;
        pos.y -= 5f;
        Debug.Log(pos);
    }

    void PlayerGridPosition()
    {
        pathfinding.GetGrid().GetXY(player.transform.position, out int x, out int y);
    }

    private void Update()
    {
        //pathfinding.GetNode((int)playerPos.x, (int)playerPos.y).x - 5, pathfinding.GetNode((int)playerPos.x - 5, (int)playerPos.y).y,
        //Vector3 playerPos = player.transform.position;
        //int pX;
        //int pY;
        //pathfinding.GetGrid().GetXY(playerPos, out pX, out pY);

        if (Input.GetMouseButtonDown(0))
        {
            //(int)player.transform.position.x, (int)player.transform.position.y
                
            //Get world position within the grid and find the path from 0,0 to where ever the mouse was clicked
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            List<PathNodeAI> path = pathfinding.FindPath(1, 1, x, y);
            Debug.Log("Path length:" + path.Count);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    AStar.squareCount++;
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5);
                }

                //player.GetComponent<PlayerAI>().SetTargetPosition(mouseWorldPosition);
                //player.GetComponent<PlayerAI>().Movement();
            }
            else
            {
                Debug.Log("No Path was found");
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
