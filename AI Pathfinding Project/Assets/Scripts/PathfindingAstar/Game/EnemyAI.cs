using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyAIMode aIMode = EnemyAIMode.Wander;
    [SerializeField] private GameObject originPoint;
    [SerializeField] private GameObject playerAI;
    public int distance = 4;

    void Start()
    {
        playerAI.GetComponent<Player>();
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity);
        Debug.DrawRay(originPoint.transform.position, Vector2.up * distance, Color.red);
  
        if (hit.collider == playerAI)
        {
            Vector3 pos = gameObject.transform.position;
            Debug.Log("Player position of "+ gameObject.name + ": " + pos);
            
        }
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
