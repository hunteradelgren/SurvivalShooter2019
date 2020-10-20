using UnityEngine;
using Mirror;

public class GameOverManager : NetworkBehaviour
{
    public PlayerHealth[] playerHealth;
	public float restartDelay = 5f;


    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        bool allDead = true;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            Debug.Log("Number of Players: " + players.Length);
            playerHealth = new PlayerHealth[players.Length];
            for (int i = 0; i < playerHealth.Length; i++)
            {
                playerHealth[i] = players[i].GetComponent<PlayerHealth>();
                if (playerHealth[i].currentHealth > 0f)
                {
                    allDead = false;
                }
            }
        }
        else
        {
            allDead = false;
        }
        if (allDead)
        {
            anim.SetTrigger("GameOver");
            Debug.Log("All Players Dead");
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				Application.LoadLevel(Application.loadedLevel);
                foreach(GameObject player in players)
                {
                    Instantiate<GameObject>(player);
                }
			}
        }
    }
}
