using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    public Transform roundBegin;
    public GameObject healthBar;

    Transform player;

    public int enemies = 4;
    bool bossRound = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossRound && player.transform.position.x > roundBegin.transform.position.x)
        {
            Debug.Log("Begin boss fight");
            Instantiate(boss, transform.position, boss.transform.rotation);
            bossRound = false;
            healthBar.SetActive(true);

        }
    }

    public void decreaseEnemy()
    {
        enemies -= 1;
        if(!bossRound && enemies == 0)
        {
            bossRound = true;
        }
    }
}
