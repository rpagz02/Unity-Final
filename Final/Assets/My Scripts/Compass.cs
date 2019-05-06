using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private GameObject Player;
    [SerializeField]
    Vector3 rotation;
    float rotationZ;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        rotation = new Vector3(0, 0, Player.transform.eulerAngles.y);
        transform.rotation = Quaternion.Euler(rotation);

    }
}
