using UnityEngine;
using System.Collections;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject[] player;
    PlayerHealth[] playerHealth;
    GameObject activePlayer;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    { 
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
            if (other.gameObject == activePlayer)
            {
                playerInRange = true;
            }
    }


    void OnTriggerExit (Collider other)
    {
            if (other.gameObject == activePlayer)
            {
                playerInRange = false;
            }
    }


    void Update ()
    {
        int count = 0;
        player = GameObject.FindGameObjectsWithTag("Player");
        playerHealth = new PlayerHealth[player.Length];
        foreach (GameObject p in player)
        {
            
            playerHealth[count] = p.GetComponent<PlayerHealth>();
            Debug.Log("Player " + count + " has " + playerHealth[count].currentHealth + " Health");
            timer += Time.deltaTime;
            activePlayer = p;
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                
                Attack(p.GetComponent<PlayerHealth>());
            }

            if (p.GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
            count++;
        }
    }


    void Attack (PlayerHealth h)
    {
        timer = 0f;

        if(h.currentHealth > 0)
        {
            h.TakeDamage (attackDamage);
        }
    }
}
