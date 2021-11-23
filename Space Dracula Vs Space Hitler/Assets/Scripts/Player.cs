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
    public float speed = 3;
    public int jumpNum = 1;
    public float jumpV = 5f;
    public bool fly = false;
    public float stamina = 5;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    public HealthBar healthBar;

    float nextAttackTime = 0f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
   

    private void Awake()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        collider = transform.GetComponent<BoxCollider2D>();
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

        Attack();

        

        if (Input.GetKeyUp(KeyCode.F) && fly == false && stamina >= 5)
        {
            fly = true;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.up * 0f;
        }
        else if(Input.GetKeyUp(KeyCode.F) && fly == true || stamina < 1)
        {
            fly = false;
            rigidBody.gravityScale = 0.42f;
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

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");


                
                nextAttackTime = Time.time + 1.9f / attackRate;
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
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<Enemy>().takeDamage(20);
        }
    }

    public void Movement()
    {
        float XInput = Input.GetAxis("Horizontal");
        float horizontalInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if(Input.GetAxis("Horizontal") > 0)transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        if (Input.GetAxis("Horizontal") < 0) transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        if (fly == true)
        {
            transform.Translate(Vector2.up * verticalInput);

        }

        transform.Translate(Vector2.right * horizontalInput);
        if (Mathf.Abs(XInput) > 0)
        {
            anim.SetInteger("StateNum", 1);
        }
        else
        {
            anim.SetInteger("StateNum", 0);
        }


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

}
