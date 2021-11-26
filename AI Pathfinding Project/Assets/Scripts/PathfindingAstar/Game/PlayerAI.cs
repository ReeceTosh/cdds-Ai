using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private HealthStation station;

    public static int healthPoints;
    public float speed;

    private int currentPathIndex;
    private List<Vector3> pathList;

    private readonly EnemyAI enemy;
    public static EnemyAI[] enemyStaged;

    void Start()
    {
        healthPoints = 10;
        enemyStaged = FindObjectsOfType<EnemyAI>();
        speed = 1;

        if (slider != null)
        {
            Display();
        }
    }
    void Update()
    {

        Display();


        //if (Input.GetKeyDown("space"))
        //{
        //    healthPoints -= 1;
        //    Debug.Log("Player has received damage: " + 1);
        //    Display();
        //}
    }

    public void Movement()
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

    public int GetPlayerHP()
    {
        return healthPoints;
    }

    void Display()
    {
        if (healthPoints > 10)
        {
            healthPoints = 10;
        }

        if (slider)
        {
            slider.value = GetPlayerHP();
            if (slider.value <= 0)
            {
                slider.value = 0;
                Application.Quit();
            }
        }
        else
        {
            //do nothing
        }
    }

    //Problem is related to this and the path not properly reading.
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathList = Pathfinding.Instance.FindPath(transform.position, targetPosition);
        if ( pathList != null && pathList.Count > 1)
        {
            pathList.RemoveAt(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected!");

        if (collision.gameObject.CompareTag("EnemyAI"))
        {
            healthPoints -= 1;
            Display();
        }

        //Bug: collision is being detected and heals player on collision, however, it does it infinitly 
        //regardless of the packsRemaining in the health station.
        if (collision.gameObject.CompareTag("HealthStation"))
        {
            healthPoints += 1;
            Debug.Log("Player has been healed");
            Display();
            station.RemovePack();
            Debug.Log("PlayerHP: " + GetPlayerHP());

        } 
    }
}
