using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog_Range : MonoBehaviour
{
    private string promptIndex;
    public GameObject dialogText;
    public GameObject dialogBox;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(promptIndex)
        {
            case "Prompt_Welcome": // Welcome Instructions
                {
                    Debug.Log("Got Into Prompt Welcome");
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                        "Welcome to the practice range. Here you can practice the skills needed to aid you in " +
                        "your future endevours. Gear up and practice at the firing range (right) before entering the training course ahead. " +
                        "This is just a demo so take your time, it won't be getting recorded.";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");
                    break;
                }
            case "Prompt_Shoot": // Shooting and ADS prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                    "Clear the room of target dummies to advance. Turrets are optional but be mindful of their damage.";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");
                    break;
                }
            case "Prompt_Cover": // Taking cover prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                        "Use the enviornment to take cover from enemy projectiles.";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");
                
                    break;
                }
            case "Prompt_Crouch": // Crouching Prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                    "Press  [C]  to crouch";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");

                    break;
                }
            case "Prompt_Steam": // Crouching Prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                    "Beware of enviornmental hazards. Avoid the steam by crouching to the left";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");

                    break;
                }
            case "Prompt_Weapons": // Crouching Prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                    "Weapons outlined in green will automatically be added to your invetory when collided with.";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");

                    break;
                }
            case "Prompt_DEMO": // Crouching Prompt
                {
                    dialogText.GetComponent<TextMeshProUGUI>().text =
                    "Welcome to the 1st playable demo. Proceed to the training gorunds to test your skills at the firing range and training course ahead.";
                    dialogBox.SetActive(true);
                    SetPromptIndex("");

                    break;
                }
        }
    }

    public void SetPromptIndex(string promptName)
    {
        promptIndex = promptName;
    }
    public string GetPromptIndex()
    {
        return promptIndex;
    }

    public void CloseDialogBox()
    {
        Debug.Log("Closing Dialog");
        dialogBox.SetActive(false);
    }
}
