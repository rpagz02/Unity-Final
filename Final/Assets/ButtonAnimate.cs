using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimate : MonoBehaviour
{
    public GameObject ObjToAnimate;


    public void MouseOverAnimation()
    {
        ObjToAnimate.GetComponent<Animator>().SetBool("Hover", true);
    }
    public void MouseOutAnimation()
    {
        ObjToAnimate.GetComponent<Animator>().SetBool("Hover", false);
    }
}
