using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health_UI : MonoBehaviour
{
    public GameObject FillImage;
    public GameObject HealthText;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FillImage.gameObject.GetComponent<Image>().fillAmount = Player.GetComponent<FPS_Player>().GetHealth() / Player.GetComponent<FPS_Player>().GetMaxHealth();
        HealthText.GetComponent<Text>().text = Player.GetComponent<FPS_Player>().GetHealth().ToString("F0") + "%";
    }
}
