using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu_UI : MonoBehaviour
{
    [Header("Upgrade Menu Tab Objects")]
    public GameObject VitalityTab;
    public GameObject AttackTab;
    public GameObject AbilityTab;
    [Space(10)]
    [Header("The Different Upgrade Menu Pages")]
    public GameObject VitalityPage;
    public GameObject AttackPage;
    public GameObject AbilityPage;
    [Space(2)]
    [Header("The Canvas Brain Object that persists through pauses")]
    public GameObject CanvasBrain;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region OnClick Methods
    public void OnVitalityClick()
    {
        AttackPage.SetActive(false);
        AbilityPage.SetActive(false);
        VitalityPage.SetActive(true);
    }
    public void OnAttackClick()
    {
        AbilityPage.SetActive(false);
        VitalityPage.SetActive(false);
        AttackPage.SetActive(true);
    }
    public void OnUtilityClick()
    {
        VitalityPage.SetActive(false);
        AttackPage.SetActive(false);
        AbilityPage.SetActive(true);
    }
    #endregion OnClick Methods
}
