using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platform;
    public GameObject camera;
    private Animator anim;

    //Player movement speed
    [Range(0, 10)]
    public float speed = 5;
    public int jumpNum = 1;
    public float jumpV = 7f;
    public float stamina = 5;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    public HealthBar healthBar;
    public GameObject knife;
    public Transform kinfeSpawn;

    public float dashDistance = 6f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    float horizontalInput;

    float nextStabTime = 0f;
    float nextThrowTime = 0f;

    private Rigidbody2D rigidBody;
    private PolygonCollider2D collider;
   

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        collider = transform.GetComponent<PolygonCollider2D>();
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        doubleTapTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !isDashing|| Input.GetKeyDown(KeyCode.Space) && jumpNum <= 1 && !isDashing)
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

        Attack();

        

       
        
    }

    public void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpV;
        
    }

    public void Attack()
    {
        if (Time.time >= nextStabTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");


                
                nextStabTime = Time.time + 1.9f / attackRate;
            }

        }
        if(Time.time >= nextThrowTime)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Instantiate(knife, kinfeSpawn.transform.position, knife.transform.rotation);

                nextThrowTime = Time.time + 1.9f / attackRate + 4;
            }
        }
    }

    public void takeDamage(float damage)
    {
        healthBar.SetHealth(healthBar.slider.value - damage);
    }

    public void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                Debug.Log("Hit " + enemy.name);
                enemy.GetComponent<Enemy>().takeDamage(20);
            }

        }
    }


    public void Movement()
    {

            float XInput = Input.GetAxis("Horizontal");
            horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            if (Input.GetAxis("Horizontal") > 0) transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);   
            if (Input.GetAxis("Horizontal") < 0) transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            if (!isDashing)
            {
                //transform.Translate(Vector2.right * horizontalInput * speed *Time.deltaTime);
                rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);
        }


        if (Mathf.Abs(XInput) > 0)
            {
                anim.SetInteger("StateNum", 1);
            }
            else
            {
                anim.SetInteger("StateNum", 0);
            }


        //Dashing Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + .5f;
                rigidBody.gravityScale = 5;
            }

            lastKeyCode = KeyCode.A;
        }

        //Dash Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1f));
                
            }
            else
            {
                doubleTapTime = Time.time + .5f;
                rigidBody.gravityScale = 5;
            }

            lastKeyCode = KeyCode.D;
        }

    }

    IEnumerator Dash (float direction)
    {
        isDashing = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        rigidBody.gravityScale = gravity;
        isDashing = false;
        
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }
}
