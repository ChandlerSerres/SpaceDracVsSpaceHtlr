using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShooter : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject laser;

    Transform Player;
    Animator anim;
    float shootTimer;
    float timerBegin = 4f;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = timerBegin;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(Player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Vector2.Distance(transform.position, Player.transform.position) <= 10f)
        {
            if(shootTimer <= 0)
            {
                anim.SetTrigger("Shoot");
                shootTimer = timerBegin;
            }
            
        }
    }

    void Shoot()
    {
        Instantiate(laser, firingPoint.transform.position, laser.transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
