using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerHealth player2Health;
    public float restartDelay = 12f;


    Animator anim;
    float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            if(player2Health != null)
            {
                if(player2Health.currentHealth <= 0)
                {
                    Debug.Log("Multiplayer Game Over");
                    anim.SetTrigger("GameOver");

                    restartTimer += Time.deltaTime;

                    if (restartTimer >= restartDelay)
                    {
                        Application.LoadLevel(Application.loadedLevel);
                    }
                }
            }
            if (player2Health == null)
            {
                Debug.Log("Single Player Game Over");
                anim.SetTrigger("GameOver");

                restartTimer += Time.deltaTime;

                if (restartTimer >= restartDelay)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            
        }
    }
}
