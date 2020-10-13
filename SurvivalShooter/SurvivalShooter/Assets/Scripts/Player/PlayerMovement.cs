using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    [SerializeField]
    ParticleSystem HitParticles;

    public int playerindex = 1;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private float count = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal" + playerindex);
        float v = Input.GetAxisRaw("Vertical" + playerindex);
        float r = Input.GetAxisRaw("Rotate" + playerindex);

        Move(h, v);
        Turning(r);
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(float r)
    {

        transform.Rotate(0, 5f * r, 0);

    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        if (walking)
        {
            if (count == 0)
            {
                Vector3 feet = new Vector3(transform.position.x, 0, transform.position.z);
                Instantiate(HitParticles, feet, Quaternion.identity).Play();
            }
            count++;
            if (count == 20)
                count = 0;
        }

        anim.SetBool("IsWalking", walking);
    }
}
