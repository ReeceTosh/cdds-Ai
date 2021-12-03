using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyAIMode aIMode = EnemyAIMode.Wander;
    private GameObject player;

    private int currentPathIndex;
    private List<Vector3> pathList;

    private TestAstarClick astarClick;

    public float speedEnemy;



    void Start()
    {
        player = FindObjectOfType<GameObject>();
        astarClick = FindObjectOfType<TestAstarClick>();
    }


    void Update()
    {
        switch (aIMode)
        {
            case EnemyAIMode.Wander:
                if (Vector3.Distance(player.transform.position, transform.position) <= astarClick.DetectionRadius)
                {
                    aIMode = EnemyAIMode.Attack;
                }
                else
                {
                    Wander();
                }
                break;

            case EnemyAIMode.Attack:
                if (Vector3.Distance(player.transform.position, transform.position) > astarClick.DetectionRadius)
                {
                    aIMode = EnemyAIMode.Wander;
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }

        //Debug.Log("State: " + aIMode);
    }

    private void AttackPlayer()
    {
        //USE VECTOR3.DISTANCE(ENEMY, PLAYER) TO RECORD THE DISTANCE BETWEEN THEM BOTH AND PERFORM A CERTAIN ACTION DEPENDING ON THE DISTANCE.
        astarClick.AstarEnemyAttack();
        MovementEnemy();

    }

    private void Wander()
    {
        astarClick.AstarEnemyRandom();
        MovementEnemy();
    }

    public enum EnemyAIMode
    {
        Wander,
        Attack
    }

    public void MovementEnemy()
    {
        //This allows movement as long as a path exists
        if (pathList != null)
        {
            Vector3 targetPosition = pathList[currentPathIndex];

            //If it is far enough or close enough to the target position
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                //Direction for movement
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                //Movement
                //float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speedEnemy * Time.deltaTime;
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

        if (pathList != null && pathList.Count > 1)
        {
            pathList.RemoveAt(0);
        }
    }
}
