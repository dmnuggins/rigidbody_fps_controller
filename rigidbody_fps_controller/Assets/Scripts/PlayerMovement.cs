using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider capsuleCol;

    public float speed = 12.0f; // speed of player
    public float acceleration = 20.0f; // units/sec accel
    public float deceleration = 20.0f; // units/sec decel
    public float airAccel = 5.0f; // mid-air accel
    public float jumpSpeed = 7.0f; // jump speed when jump button is pressed
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public float maxSlope = 45.0f; // max slope player can traverse

    private Vector3 jumpVector;

    private bool grounded = false; // variable checked if player is grounded
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        rb.AddForce(move * speed * Time.deltaTime, ForceMode.VelocityChange);

        if(Input.GetButton("Jump") && grounded)
        {
            rb.velocity = Vector3.up * jumpSpeed;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            grounded = false;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            grounded = true;
            Debug.Log("Grounded -> TRUE");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            grounded = false;
            Debug.Log("Grounded -> FALSE");
        }
    }
}
