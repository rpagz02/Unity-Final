using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu_UI : MonoBehaviour
{
    [Header("The Menus corresponding to their respective tabs")]
    public GameObject DetailsMenu;
    public GameObject UpgradeMenu;
    public GameObject InventoryMenu;
    public GameObject TutorialMenu;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region In Game Menu On-Click Events
    // On Click ...
    //  Deactivate all pages then,
    //  Activate Button's corresponding page
    public void OnDetailsClick()
    {
        for(int i = 0; i < 4; i++)
        {
            DetailsMenu.SetActive(false);
            UpgradeMenu.SetActive(false);
            InventoryMenu.SetActive(false);
            TutorialMenu.SetActive(false);
        }
        DetailsMenu.SetActive(true);

    }
    public void OnUpgradeClick()
    {
        for (int i = 0; i < 4; i++)
        {
            DetailsMenu.SetActive(false);
            UpgradeMenu.SetActive(false);
            InventoryMenu.SetActive(false);
            TutorialMenu.SetActive(false);
        }
        UpgradeMenu.SetActive(true);
    }
    public void OnInventoryClick()
    {
        for (int i = 0; i < 4; i++)
        {
            DetailsMenu.SetActive(false);
            UpgradeMenu.SetActive(false);
            InventoryMenu.SetActive(false);
            TutorialMenu.SetActive(false);
        }
        InventoryMenu.SetActive(true);
    }
    public void OnTutorialClick()
    {
        for (int i = 0; i < 4; i++)
        {
            DetailsMenu.SetActive(false);
            UpgradeMenu.SetActive(false);
            InventoryMenu.SetActive(false);
            TutorialMenu.SetActive(false);
        }
        TutorialMenu.SetActive(true);
    }
    #endregion  In Game Menu On-Click Events
}
