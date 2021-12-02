using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyAIMode aIMode = EnemyAIMode.Wander;
    [SerializeField] private GameObject originPoint;
    [SerializeField] private GameObject enemyAI;
    private GameObject player;

    private int currentPathIndex;
    private List<Vector3> pathList;

    private AStar pathfind;
    private TestAstarClick astarClick;

    public int distance = 4;
    public float speed = 25;

    int enemyX;
    int enemyY;
    int playerX;
    int playerY;

    void Start()
    {
        player = FindObjectOfType<GameObject>();
        astarClick = FindObjectOfType<TestAstarClick>();

        enemyX = (int)enemyAI.gameObject.transform.localPosition.x;
        enemyY = (int)enemyAI.gameObject.transform.localPosition.y;
        playerX = (int)player.gameObject.transform.localPosition.x;
        playerY = (int)player.gameObject.transform.localPosition.y;
    }


    void Update()
    {
        switch (aIMode)
        {
            case EnemyAIMode.Wander:
                Wander();
                break;

            case EnemyAIMode.Attack:
                AttackPlayer();
                break;
        }
        //Debug.Log(aIMode + " is on.");
    }

    //Need Functions to complete state-machine after A* is set up:
    //(optional) SearchPlayer(targets last known position),
    //Wander(go to a spot randomly),
    //AttackPlayer(targets position, raycast range and damage)
    private void AttackPlayer()
    {
        //USE VECTOR3.DISTANCE(ENEMY, PLAYER) TO RECORD THE DISTANCE BETWEEN THEM BOTH AND PERFORM A CERTAIN ACTION DEPENDING ON THE DISTANCE.

        //The attack state needs to have a raycast that displays the field of view while finding the player location 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity);
        Debug.DrawRay(originPoint.transform.position, Vector2.up * distance, Color.red);

        if (hit.collider == player)
        {
            Vector3 pos = gameObject.transform.position;
            Debug.Log("Player position of " + gameObject.name + ": " + pos);
        }

        pathfind.FindPath(enemyX, enemyY, playerX, playerY);
    }

    private void Wander()
    {
        astarClick.AstarEnemyRandom();
        MovementEnemy();
    }

    public enum EnemyAIMode
    {
        Wander,
        Attack,
        Search
    }

    public void MovementEnemy()
    {
        //This allows movement as long as a path exists
        if (pathList != null)
        {
            Vector3 targetPosition = pathList[currentPathIndex];

            //foreach(PathNodeAI node in pathList)
            //{
            //    Debug.Log("");
            //}

            //If it is far enough or close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                //Direction for movement
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                //Movement
                //float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathList.Count)
                {
                    pathList = null;
                }
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        pathList = AStar.Instance.FindPath(transform.position, targetPosition);
        currentPathIndex = 0;
        //pathList = AStar.Instance.FindPath(transform.position, targetPosition);


        if (pathList != null && pathList.Count > 1)
        {
            pathList.RemoveAt(0);
        }
    }
}
