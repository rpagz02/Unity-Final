using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStatus : MonoBehaviour
{
    private bool isDead = false;


    public void SetDeathStatus(bool status)
    {
        isDead = status;
    }
}
