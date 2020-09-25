using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12.0f; // speed of player
    public float jumpSpeed = 7.0f; // jump speed when jump button is pressed
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    private Rigidbody rb;
    private CapsuleCollider capsuleCol;
    private float x_axis, z_axis;

    private bool grounded = false; // variable checked if player is grounded
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCol = GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {

        #region Controller Input
        x_axis = Input.GetAxis("Horizontal");
        z_axis = Input.GetAxis("Vertical");

        rb.MovePosition(transform.position + Time.deltaTime * speed * transform.TransformDirection(x_axis, 0f, z_axis));
        #endregion

        #region Jump Input
        // Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = Vector3.up * jumpSpeed;
        }
        // Jump acceleration modifier
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
        #endregion

    }

    private void FixedUpdate()
    {
        
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
