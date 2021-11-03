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
        //This allows movement
        var movementHorizontal = Input.GetAxis("Horizontal");
        var movementVertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(movementHorizontal, movementVertical, 0) * Time.deltaTime * speed;

        Display();

        if (Input.GetKeyDown("space"))
        {
            healthPoints -= 1;
            Debug.Log("Player has received damage: " + 1);
            Display();
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
