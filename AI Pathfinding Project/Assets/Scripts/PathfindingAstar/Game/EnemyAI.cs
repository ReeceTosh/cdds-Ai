using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyAIMode aIMode = EnemyAIMode.Wander;
    [SerializeField] private GameObject originPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyAI;

    private AStar pathfind;
    public int distance = 4;

    int enemyX;
    int enemyY;
    int playerX;
    int playerY;

    void Start()
    {
        if (player != null)
            player.GetComponent<Player>();

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

    }

    public enum EnemyAIMode
    {
        Wander,
        Attack,
        Search
    }
}
