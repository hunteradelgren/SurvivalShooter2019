using UnityEngine;
using System.Collections;
using Mirror;

public class EnemyMovement : NetworkBehaviour
{
    Transform Targetplayer;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        SetClosestPlayer();
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (Targetplayer.position);
        }
        else
        {
            nav.enabled = false;
        }
    }

    void SetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform[] playerPos = new Transform[players.Length];
        int count = 0;
        foreach (GameObject player in players)
        {
            playerPos[count] = player.transform;
            count++;
        }
        if(players.Length > 0)
        {
            Transform closest = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Transform spot in playerPos)
            {
                float dist = Vector3.Distance(spot.position, currentPos);
                if (dist < minDist)
                {
                    closest = spot;
                    minDist = dist;
                }
            }
            Targetplayer = closest;
            playerHealth = closest.gameObject.GetComponent<PlayerHealth>();
        }
    }
}
