using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 10f;
    public bool SpinRight = true;

    void Update()
    {
        if(SpinRight)
        transform.Rotate(new Vector3(0,0,1), speed * Time.deltaTime);
        else
            transform.Rotate(new Vector3(0, 0, -1), speed * Time.deltaTime);

    }
}
