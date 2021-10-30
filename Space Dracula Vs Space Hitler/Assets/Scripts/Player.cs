using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platform;
    public GameObject camera;

    //Player movement speed
    [Range(0, 10)]
    public float speed = 3;
    public int jumpNum = 1;
    public float jumpV = 5f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
   

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        collider = transform.GetComponent<BoxCollider2D>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space) && jumpNum <= 1)
        {
            if (IsGrounded())
            {
                jumpNum = 1;
            }
            else
            {
                jumpNum += 1;
            }
            Jump();
        }
    }

    public void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpV;
        
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        

        transform.Translate(Vector2.right * horizontalInput);
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size , 0f, Vector2.down , 0.1f, platform);
        return raycastHit.collider != null;
    }
}
