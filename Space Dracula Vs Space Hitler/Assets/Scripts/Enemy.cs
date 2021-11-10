using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;

    public Transform[] path;
    private int location;
    private float waitTime;
    public float startWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        location = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[location].position.x, transform.position.y), speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, path[0].position) < 0.99f)
        {
            if (waitTime <= 0) { location = 1; waitTime = startWaitTime; }
            else waitTime -= Time.deltaTime;

        }
        else if(Vector2.Distance(transform.position, path[1].position) < 0.99f)
        {
            if (waitTime <= 0) { location = 0; waitTime = startWaitTime; }
            else waitTime -= Time.deltaTime;
        }
    }

}
