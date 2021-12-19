using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField] private LayerMask platform;

    public float timerStart = 10f;
    public GameObject player;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

   

    private Animator anim;
    private bool canAttack = true;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;


    bool slash = false;
    bool airSlash = false;
    bool attacking = false;

    Vector3 playerPos;


    float nextTimeToAttack;


    private void Awake()
    {
        nextTimeToAttack = timerStart;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(!canAttack) nextTimeToAttack -= Time.deltaTime;
        if(nextTimeToAttack <= 0)
        {
            nextTimeToAttack = timerStart;
            canAttack = true;
        }
        if (canAttack && IsGrounded() && !attacking)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            int randomAttack = Random.Range(0, 2);
            Debug.Log("random:" + randomAttack);
            if(randomAttack == 1)
            {
                slash = true;
                attacking = true;
                playerPos = player.transform.position;
                playerPos.x += 1;
                playerPos.y = transform.position.y;
               

            }else if(randomAttack == 0)
            {
                Debug.Log("AirSlashing");
                airSlash = true;
                attacking = true;
                
            }
            
        }
        if (!attacking)
        {
            if(player.transform.position.x > transform.position.x) { transform.eulerAngles = new Vector3(0, 0, 0); }
            if (player.transform.position.x < transform.position.x) { transform.eulerAngles = new Vector3(0, 180, 0); }
        }
        SlashAttack();
        AirSlash();
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platform);
        return raycastHit.collider != null;
    }

    void SlashAttack()
    {
        if (slash)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 8f * Time.deltaTime);
            if(Vector2.Distance(transform.position,playerPos) <= attackRange)
            {
                Attack();
                Debug.Log("attacking");
                slash = false;
                canAttack = false;
                attacking = false;
            }
        }
    }

    void AirSlash()
    {
        if (airSlash)
        {
            JumpAnim();
            Debug.Log("airattacking");
        }

    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void AirAttack()
    {
        anim.SetTrigger("AirAttack");
        
        canAttack = false;
        attacking = false;

    }

    void JumpAnim()
    {
        anim.SetTrigger("Jump");
        
    }

    void Jump()
    {
        float dropAttackDir = ( player.transform.position.x - transform.position.x);
        rigidBody.AddForce(new Vector2(dropAttackDir, 10f), ForceMode2D.Impulse);
        airSlash = false;
    }

    public void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);

            if(enemy.name == "Player")
            {
                player.GetComponent<Player>().takeDamage(0.2f);
            }
           
        }


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
 