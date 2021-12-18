﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            DestroyObject(gameObject);
            Debug.Log("Dead");
        }

    }
}
