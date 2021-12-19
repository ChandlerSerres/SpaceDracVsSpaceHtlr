using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    Vector3 localScale;
    EnemyHealth health;
    float startX;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        health = gameObject.GetComponentInParent<EnemyHealth>();
        startX = localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (health.health/100)*startX;
        transform.localScale = localScale;
    }
}
