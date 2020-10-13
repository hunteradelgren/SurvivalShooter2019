using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCoOp : MonoBehaviour
{
    public EnemyManager bunnies;
    public EnemyManager bears;
    public EnemyManager elephants;

    public PlayerHealth Player1Health;
    public Slider healthSlider;
    public Image heart;

    public GameOverManager gameOver;

    public GameObject playerHolder;
    public bool addedPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float p = Input.GetAxisRaw("SpawnPlayer");
        if (!addedPlayer && p == 1)
        {
            GameObject Player2 = Instantiate(playerHolder, new Vector3(1, 0, 1), Quaternion.identity);
            Player2.GetComponentInChildren<PlayerMovement>().playerindex = 2;
            Player2.GetComponentInChildren<PlayerShooting>().playerindex = 2;
            Player2.GetComponentInChildren<PlayerHealth>().Player2Health = Player1Health;
            Player2.GetComponentInChildren<PlayerHealth>().healthSlider = healthSlider;
            healthSlider.transform.position = new Vector3(400, 30, 30);
            heart.transform.position = new Vector3(300, 30, 30);

            playerHolder = Player2;
            addedPlayer = true;
            bunnies.player2Health = playerHolder.GetComponentInChildren<PlayerHealth>();
            bears.player2Health = playerHolder.GetComponentInChildren<PlayerHealth>();
            elephants.player2Health = playerHolder.GetComponentInChildren<PlayerHealth>();
            gameOver.player2Health = playerHolder.GetComponentInChildren<PlayerHealth>();
            Player1Health.Player2Health = playerHolder.GetComponentInChildren<PlayerHealth>();
        }
    }
}
