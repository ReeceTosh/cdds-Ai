using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStation : MonoBehaviour
{
    public int packsRemaining;
    [SerializeField] private Player player;

    void Start()
    {
        packsRemaining = 5;
    }

    public int GetPackRemaining()
    {
        return packsRemaining;
    }

    public void RemovePack()
    {
        if (packsRemaining > 1)
        {
            packsRemaining -= 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
