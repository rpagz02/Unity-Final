using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsMenu_UI : MonoBehaviour
{
    [Header("The Text Attriubutes for each Stat")]
    public GameObject HealthStatText;
    public GameObject ArmorStatText;
    [Header("Drag and Drop the Current Level Image Object Here")]
    public GameObject LevelImage;
    [Header("The Canvas Brain Object that persists through pauses")]
    public GameObject CanvasBrain;

    private GameObject Player;
    string PlayerHealth, PlayerArmor;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        HealthStatText.GetComponent<Text>().text = CanvasBrain.GetComponent<Pause_UI>().GetPlayerCurHealth().ToString() + "%";
        ArmorStatText.GetComponent<Text>().text = CanvasBrain.GetComponent<Pause_UI>().GetPlayerCurArmor().ToString() + "%";
    }

}
