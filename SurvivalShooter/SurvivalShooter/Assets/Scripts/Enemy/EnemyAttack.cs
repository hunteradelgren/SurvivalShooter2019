using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    public GameObject player;
    public GameObject player2;
    PlayerHealth playerHealth;
    public PlayerHealth player2Health;
    EnemyHealth enemyHealth;
    bool playerInRange;
    public bool player2InRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
        if (other.gameObject == player2)
        {
            player2InRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
        if (other.gameObject == player2)
        {
            player2InRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (timer >= timeBetweenAttacks && player2InRange && enemyHealth.currentHealth > 0)
        {
            Attack2();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }

        if(player2Health!=null)
        if (player2Health.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void Attack2()
    {
        timer = 0f;

        if (player2Health.currentHealth > 0)
        {
            player2Health.TakeDamage(attackDamage);
        }
    }
}
