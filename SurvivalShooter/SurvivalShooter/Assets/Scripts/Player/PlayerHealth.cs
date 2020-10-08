using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{

    public CameraFollow cam;

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    [SerializeField]
    AudioSource heartAudio;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        cam.shake = true;

        if(currentHealth <= (startingHealth/4))
        {
            heartAudio.Play();
        }

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        Timer timer1 = new Timer(5f,SpeedTime);
        TimeManager.instance.timers.Add(timer1);
        Time.timeScale = .25f;
        isDead = true;

        playerShooting.DisableEffects ();
        heartAudio.Stop();
        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    void SpeedTime()
    {
        Time.timeScale = 1;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
