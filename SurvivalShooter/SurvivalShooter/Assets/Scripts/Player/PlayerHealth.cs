using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Mirror;
using System.Linq;

public class PlayerHealth : NetworkBehaviour
{
    public int startingHealth = 100;

    PlayerHealth[] playerHealth;

    [SyncVar]
    public int currentHealth;

    public GameObject HUDCanvas;
    Slider healthSlider;
    Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            GameObject newPlayerHUD = Instantiate<GameObject>(HUDCanvas);
            healthSlider = newPlayerHUD.GetComponentInChildren<Slider>();
            damageImage = newPlayerHUD.GetComponentInChildren<Image>();
        }
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        bool allDead = true;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
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
            SceneManager.LoadScene(0);
        }
    }
}
