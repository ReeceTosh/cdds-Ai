using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class TestAstarClick : MonoBehaviour
{

    [SerializeField] private PlayerAI playerPrefab;
    [SerializeField] private EnemyAI enemyPrefab;

    private PlayerAI player;
    private EnemyAI enemy;

    private AStar pathfinding;

    private int xAxisGrid = 12;
    private int yAxisGrid = 12;

    private float wanderTimer = 0.0f;

    public float DetectionRadius = 100;
    public float FightRadius = 7;

    void Start()
    {
        pathfinding = new AStar(xAxisGrid, yAxisGrid, Vector3.zero);
        //pathfinding.GetNode(0, 1).SetIsWalkable(!pathfinding.GetNode(0, 1).isWalkable);
        MapGeneration();

        SpawnEnemy();
        SpawnPlayer();

        player = FindObjectOfType<PlayerAI>();
        enemy = FindObjectOfType<EnemyAI>();
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
                           new Vector2( 2, 5 ), new Vector2( 4, 5 ), new Vector2( 6, 5 ), new Vector2( 8, 5 ), new Vector2( 2, 4 ), new Vector2( 9, 4 ), new Vector2( 2, 3 ), new Vector2( 3, 3 ), new Vector2( 5, 3 ), new Vector2( 6, 3 ),
                           new Vector2( 8, 3 ), new Vector2( 3, 2 ), new Vector2( 7, 2 ), new Vector2( 5, 1 ), new Vector2( 9, 1 ) };

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
            SpawnEnemy();
            return;
        }
        else
        {
            pos = pathfinding.GetGrid().GetWorldPosition(x, y);
            pos.x += 5f;
            pos.y += 5f;

            EnemyAI clone = Instantiate(enemyPrefab);
            clone.transform.position = pos;

            pos.x -= 5f;
            pos.y -= 5f;
            Debug.Log(pos);
        }
    }

    public void AstarEnemyAttack()
    {
        if (Vector3.Distance(player.transform.position, enemy.transform.position) <= DetectionRadius)
        {
            Debug.Log("target locked");

            int x = (int)player.transform.position.x / (int)AStar.Instance.GetGrid().GetCellSize();
            int y = (int)player.transform.position.y / (int)AStar.Instance.GetGrid().GetCellSize();

            Vector3 pos = AStar.Instance.GetGrid().GetWorldPosition(x, y);
            enemy.GetComponent<EnemyAI>().SetTargetPosition(pos);
        }

        if (Vector3.Distance(player.transform.position, enemy.transform.position) <= FightRadius)
        {
            if (wanderTimer < 2.0f)
            {
                wanderTimer += Time.deltaTime;
                return;
            }
            wanderTimer = 0.0f;

            player.healthPoints--;
            Debug.Log("Fear not for I AM HERE.");
        }


    }

    public void AstarEnemyRandom()
    {
        //Timer for the update loop to execute code every few seconds
        if (wanderTimer < 2.0f)
        {
            wanderTimer += Time.deltaTime;
            return;
        }
        wanderTimer = 0.0f;

        for (int i = 0; i < 10; i++)
        {
            int randomX;
            int randomY;

            CordGeneration(out randomX, out randomY);

            if (AStar.Instance.GetNode(randomX, randomY).isWalkable)
            {
                Vector3 pos = AStar.Instance.GetGrid().GetWorldPosition(randomX, randomY);
                enemy.GetComponent<EnemyAI>().SetTargetPosition(pos);
                break;
            }
        }
    }

    void SpawnPlayer()
    {
        Vector3 pos = pathfinding.GetGrid().GetWorldPosition(1, 1);
        pos.x += 5f;
        pos.y += 5f;

        PlayerAI clone = Instantiate(playerPrefab);
        clone.transform.position = pos;

        pos.x -= 5f;
        pos.y -= 5f;
        Debug.Log(pos);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Get world position within the grid and find the path from 0,0 to where ever the mouse was clicked
            Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();

            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

            if (pathfinding.GetNode(x, y).isWalkable)
            {
                player.GetComponent<PlayerAI>().SetTargetPosition(mouseWorldPosition);
            }
            else
            {
                Debug.Log("ERROR: node is not walkable / no path was found.");
            }

        }

        //DEBUG ONLY!!!

        //Sets up grid with walkable tiles
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 mouseWorldPosition = UtilityClass.GetMouseWorldPosition();
        //    pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

        //    if (pathfinding.GetNode(x, y) == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        //    }
        //}
    }
}
