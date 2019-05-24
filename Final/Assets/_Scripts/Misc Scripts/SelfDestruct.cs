using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float LifeTime = 1.5f;
    private float timer;



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >LifeTime)
        Destroy(this.gameObject);
            

    }
}
