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
    public bool fly = false;
    public float stamina = 5;

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
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && fly == false || Input.GetKeyDown(KeyCode.Space) && jumpNum <= 1 && fly == false)
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

        if (Input.GetKeyUp(KeyCode.F) && fly == false && stamina >= 5)
        {
            fly = true;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.up * 0f;
        }
        else if(Input.GetKeyUp(KeyCode.F) && fly == true || stamina < 1)
        {
            fly = false;
            rigidBody.gravityScale = 1;
        }

        if(fly == true && stamina > 0)
        {

            DecreaseStam();
     
        }
        else if(fly == false && stamina < 5)
        {

            RegenStamina();
            
        }
        
    }

    public void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpV;
        
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if (fly == true)
        {
            transform.Translate(Vector2.up * verticalInput);

        }

        transform.Translate(Vector2.right * horizontalInput);
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size , 0f, Vector2.down , 0.1f, platform);
        return raycastHit.collider != null;
    }

    public void DecreaseStam()
    {
        stamina -= Time.deltaTime;
        Debug.Log("Flying: " + stamina);
    }

    public void RegenStamina()
    {
        stamina += Time.deltaTime;
        Debug.Log("Grounded: " + stamina);
    }
}
