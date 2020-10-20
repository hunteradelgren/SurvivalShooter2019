using UnityEngine;
using Mirror;

public class EnemyManager : NetworkBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    PlayerHealth[] playerHealth;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Number of Players: " + players.Length);
        if (players.Length > 0)
        {
            Debug.Log("Spawning Enemy");
            playerHealth = new PlayerHealth[players.Length];
            for (int i = 0; i < playerHealth.Length; i++)
            {
                playerHealth[i] = players[i].GetComponent<PlayerHealth>();
                if (playerHealth[i].currentHealth <= 0f)
                {
                    return;
                }
            }

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
