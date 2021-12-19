using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    Player player;
    BossSpawn bs;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bs = GameObject.FindGameObjectWithTag("BossSpawn").GetComponent<BossSpawn>();
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
            player.addHealth(.2f);
            bs.decreaseEnemy();
            Destroy(gameObject);
        }

    }
}
