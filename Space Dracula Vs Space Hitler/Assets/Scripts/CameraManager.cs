﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public Vector2 followOffset;
    public float speed = 5.05f;
    private Vector2 threshhold;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        threshhold = calculateThreshhold();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(target);
    }

    private void LateUpdate()
    {
        if (player.transform.position.x > 3.5 && player.transform.position.x < 132.75)
        {

            Vector2 follow = player.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshhold.x)
            {
                newPosition.x = follow.x;
            }


           newPosition.y = follow.y + .7f;


            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;

            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
    }

    private Vector3 calculateThreshhold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshhold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
