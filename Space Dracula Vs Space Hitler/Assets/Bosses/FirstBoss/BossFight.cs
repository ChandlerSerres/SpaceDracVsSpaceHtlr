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
        if (canAttack && IsGrounded())
        {
            Random.seed = System.DateTime.Now.Millisecond;
            int randomAttack = Random.Range(0, 2);
            if(randomAttack == 1)
            {
                Attack();
                Debug.Log("attacking");

            }else if(randomAttack == 0)
            {
                JumpAnim();
                Debug.Log("airattacking");
            }
            canAttack = false;
        }
        
        
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platform);
        return raycastHit.collider != null;
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void AirAttack()
    {
        anim.SetTrigger("AirAttack");
        
        
    }

    void JumpAnim()
    {
        anim.SetTrigger("Jump");
        
    }

    void Jump()
    {
        float dropAttackDir = ( player.transform.position.x - transform.position.x);
        Debug.Log(dropAttackDir);
        rigidBody.AddForce(new Vector2(dropAttackDir, 6f), ForceMode2D.Impulse);
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
 