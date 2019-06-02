using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    private int totalEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Enemies != null)
            totalEnemies = Enemies.Length;
    }
    private void Update()
    {
        if (Enemies != null)
            totalEnemies = Enemies.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if (totalEnemies != 0)
            {
                for (int i = 0; i < totalEnemies; i++)
                {
                    Enemies[i].SetActive(true);
                }
            }
        }
    }
}
