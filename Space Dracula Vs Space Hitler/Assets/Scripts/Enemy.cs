using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;

    public Transform[] path;
    public Transform player;
    public Player playerObj;
    private int location;
    private float waitTime;
    public float startWaitTime;
    private Rigidbody2D rigidBody;
    private bool nearPlayer = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    private Animator anim;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        location = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Vector2.Distance(transform.position, player.position) <= attackRange + 3f)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
           

            anim.SetTrigger("Attack");



            nextAttackTime = Time.time + 1.9f / attackRate;
        }

    }

    public void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);

            playerObj.takeDamage(0.05f);
        }

        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!nearPlayer)
        {
            if (collision.transform == path[0]) { transform.eulerAngles = new Vector3(0, 180, 0); }
            else { transform.eulerAngles = new Vector3(0, 0, 0); }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Movement()
    {
        if (Vector2.Distance(transform.position, player.position) < 4f)
        {
            nearPlayer = true;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
            if (transform.position.x > player.position.x) transform.eulerAngles = new Vector3(0, 180, 0);
            else { transform.eulerAngles = new Vector3(0, 0, 0); }
        }
        else if (nearPlayer == true)
        {
            nearPlayer = false;
        }

        if (nearPlayer == false)
        {

            if (transform.position.x > path[0].transform.position.x && transform.position.x > path[1].transform.position.x) { transform.eulerAngles = new Vector3(0, 180, 0); }
            if (transform.position.x < path[0].transform.position.x && transform.position.x < path[1].transform.position.x) { transform.eulerAngles = new Vector3(0, 0, 0); }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[location].position.x, transform.position.y), speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, path[0].position) < 0.99f)
            {
                if (waitTime <= 0) { location = 1; waitTime = startWaitTime; }
                else waitTime -= Time.deltaTime;


            }
            else if (Vector2.Distance(transform.position, path[1].position) < 0.99f)
            {
                if (waitTime <= 0) { location = 0; waitTime = startWaitTime; }
                else waitTime -= Time.deltaTime;
            }



        }
    }
}
