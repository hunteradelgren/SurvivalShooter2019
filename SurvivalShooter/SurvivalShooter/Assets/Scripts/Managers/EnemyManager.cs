using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerHealth player2Health;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public bool switched = true;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        bool bothDead = false;
        if (playerHealth.currentHealth <= 0f)
        {
            bothDead = true;
        }

        if (player2Health != null)
        {
            if (player2Health.currentHealth > 0f)
            {
                bothDead = false;
            }
        }

        if (bothDead)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemies = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        if (player2Health != null)
        {
            enemies.GetComponent<EnemyAttack>().player2 = player2Health.gameObject;
            enemies.GetComponent<EnemyAttack>().player2Health = player2Health;

            if (player2Health.currentHealth > 0 && switched)
            {
                enemies.GetComponent<EnemyMovement>().player = player2Health.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowing = playerHealth.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowingHealth = playerHealth;
                switched = false;
            }
            else if (playerHealth.currentHealth > 0)
            {
                enemies.GetComponent<EnemyMovement>().player = playerHealth.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowing = player2Health.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowingHealth = player2Health;
                switched = true;
            }
            else if(playerHealth.currentHealth <= 0 && player2Health.currentHealth > 0)
            {
                enemies.GetComponent<EnemyMovement>().player = player2Health.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowing = player2Health.GetComponentInParent<Transform>();
                enemies.GetComponent<EnemyMovement>().notFollowingHealth = player2Health;
            }
        }
    }
}
