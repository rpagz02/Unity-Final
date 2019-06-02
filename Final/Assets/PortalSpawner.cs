using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    private float timer = 0;
    private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 2 && canSpawn)
        {
            GameObject SpawnedEnemy = Instantiate(Enemies[0], this.transform.position, this.transform.rotation);
            canSpawn = false;
        }
        if(timer>=4.5)
        {
            Destroy(this.gameObject);
        }
    }
}
