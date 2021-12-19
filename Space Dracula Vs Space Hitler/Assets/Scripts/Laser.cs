using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Transform player;
    Vector2 playerPos;
    Vector2 prevPos;
    Vector3 dir;
    float speed = 65f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = player.transform.position;
        prevPos = transform.position;
        dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Player" || collision.name == "Ground")
        {
            if(collision.name == "Player")
            {
                collision.gameObject.GetComponent<Player>().takeDamage(.15f);
            }
            Destroy(gameObject);
        }
    }

}
