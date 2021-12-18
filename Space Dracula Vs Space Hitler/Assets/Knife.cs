using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class Knife : MonoBehaviour
{
    Transform player;
    Vector3 pos;
    Vector3 dir;
    Vector3 playerPos;
    private Animator anim;
    private float speed = 100f;
    float timer = 0.7f;
    bool returnedToPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = UtilsClass.GetMouseWorldPosition();
        dir = (pos - gameObject.transform.position).normalized;
        dir.z = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaSpeed = speed * Time.deltaTime;
        if (timer <= 0 || returnedToPlayer)
        {

            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            dir = (playerPos - gameObject.transform.position).normalized;
            dir.z = 0;
            returnedToPlayer = true;
            anim.SetTrigger("return");
        }
        gameObject.transform.position += dir * deltaSpeed;
        timer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Knife collided with " + collision.name);
        if (collision.tag == "Enemy")
        {
            returnedToPlayer = true;
            collision.GetComponent<EnemyHealth>().takeDamage(20);
            Debug.Log("Damage taken");
            
            
        }
        if (collision.tag == "Player" && returnedToPlayer == true)
        {
            Destroy(gameObject);
        }
            
    }
}
