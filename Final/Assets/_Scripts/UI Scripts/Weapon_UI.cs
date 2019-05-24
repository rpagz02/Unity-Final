using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_UI : MonoBehaviour
{
    public Sprite[] WeaponSprites;
    public GameObject WeaponImage;
    public GameObject AmmoText;
    public GameObject MaxAmmoImage;
    private GameObject Player;
    private GameObject ArmsWeapons;
    private GameObject Weapon;

    [SerializeField]
    private int ActiveWeapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ArmsWeapons = GameObject.FindGameObjectWithTag("Arms_Weapons");
    }

    // Update is called once per frame
    void Update()
    {
        SpriteSwitching();

        switch (ActiveWeapon_Index)
        {
            case 0: // If the Knife is Active
                {
                    Weapon = ArmsWeapons.transform.Find("Arms_Knife").gameObject;
                    AmmoText.GetComponent<Text>().text = "∞";
                    break;
                }
            case 1: // If the Pistol is Active
                {
                    Weapon = ArmsWeapons.transform.Find("Arms_Pistol").gameObject;
                    AmmoText.GetComponent<Text>().text =
                        Weapon.GetComponent<Pistol>().GetCurrentAmmo().ToString("F0") + "/" +
                        Weapon.GetComponent<Pistol>().GetMaxAmmo().ToString("F0");
                    MaxAmmoImage.GetComponent<Image>().fillAmount = (float)Weapon.GetComponent<Pistol>().GetCurrentAmmo() / Weapon.GetComponent<Pistol>().GetMaxAmmo();

                    break;
                }
            case 2: // If the SMG is Active
                {
                    Weapon = ArmsWeapons.transform.Find("Arms_SMG").gameObject;
                    AmmoText.GetComponent<Text>().text =
                        Weapon.GetComponent<SMG>().GetCurrentAmmo().ToString("F0") + "/" +
                        Weapon.GetComponent<SMG>().GetMaxAmmo().ToString("F0");
                    MaxAmmoImage.GetComponent<Image>().fillAmount = (float)Weapon.GetComponent<SMG>().GetCurrentAmmo() / Weapon.GetComponent<SMG>().GetMaxAmmo();

                    break;
                }
            case 3: // If the Shotgun is Active
                {
                    Weapon = ArmsWeapons.transform.Find("Arms_Shotgun").gameObject;
                    AmmoText.GetComponent<Text>().text =
                        Weapon.GetComponent<Shotgun>().GetCurrentAmmo().ToString("F0") + "/" +
                        Weapon.GetComponent<Shotgun>().GetMaxAmmo().ToString("F0");
                    MaxAmmoImage.GetComponent<Image>().fillAmount = (float)Weapon.GetComponent<Shotgun>().GetCurrentAmmo() / Weapon.GetComponent<Shotgun>().GetMaxAmmo();

                    break;
                }
            case 4: // If the Rifle is Active
                {
                    Weapon = ArmsWeapons.transform.Find("Arms_Rifle").gameObject;
                    AmmoText.GetComponent<Text>().text =
                        Weapon.GetComponent<Rifle>().GetCurrentAmmo().ToString("F0") + "/" +
                        Weapon.GetComponent<Rifle>().GetMaxAmmo().ToString("F0");
                    MaxAmmoImage.GetComponent<Image>().fillAmount = (float)Weapon.GetComponent<Rifle>().GetCurrentAmmo() / Weapon.GetComponent<Rifle>().GetMaxAmmo();

                    break;
                }
        }


    }

    void SpriteSwitching()
    {
        if (Player.GetComponent<FPS_WeaponHandling>().GetActiveWeaponIndex() == 0)
        {
            WeaponImage.GetComponent<Image>().sprite = WeaponSprites[0];
            ActiveWeapon_Index = 0;
        }
        else if (Player.GetComponent<FPS_WeaponHandling>().GetActiveWeaponIndex() == 1)
        {
            WeaponImage.GetComponent<Image>().sprite = WeaponSprites[1];
            ActiveWeapon_Index = 1;
        }
        else if (Player.GetComponent<FPS_WeaponHandling>().GetActiveWeaponIndex() == 2)
        {
            WeaponImage.GetComponent<Image>().sprite = WeaponSprites[2];
            ActiveWeapon_Index = 2;
        }
        else if (Player.GetComponent<FPS_WeaponHandling>().GetActiveWeaponIndex() == 3)
        {
            WeaponImage.GetComponent<Image>().sprite = WeaponSprites[3];
            ActiveWeapon_Index = 3;
        }
        else if (Player.GetComponent<FPS_WeaponHandling>().GetActiveWeaponIndex() == 4)
        {
            WeaponImage.GetComponent<Image>().sprite = WeaponSprites[4];
            ActiveWeapon_Index = 4;
        }
    }

}
