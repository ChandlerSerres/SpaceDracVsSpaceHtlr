using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    int timerBegin = 8;
    float timer = 8;
    public GameObject meteor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //-25 110
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }else if(timer <= 0)
        {
            SpawnMeteor();
            timer = timerBegin;
        }

    }

    void SpawnMeteor()
    {
        float randomX = Random.Range(0, 110);
        Vector2 pos = new Vector2(randomX, 50);

        Instantiate(meteor, pos, meteor.transform.rotation);
    }
}
