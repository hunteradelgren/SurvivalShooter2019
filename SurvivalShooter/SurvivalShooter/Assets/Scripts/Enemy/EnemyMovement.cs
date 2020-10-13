using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Transform notFollowing;
    public PlayerHealth playerHealth;
    public PlayerHealth notFollowingHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else if(notFollowingHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            nav.SetDestination(notFollowing.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
