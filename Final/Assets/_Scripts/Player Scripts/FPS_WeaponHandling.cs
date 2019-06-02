using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_WeaponHandling : MonoBehaviour
{
    public enum Weapon { Knife, Pistol, SMG, Shotgun, Rifle, LMG, Crossbow, GrenadeLauncher, Axe, Grenade, Flashlight };

    #region Variables
    [Header("Arms And Weapons")]
    public GameObject ArmsWeapons;
    [SerializeField]
    private GameObject[] WeaponInventory;
    [SerializeField]
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        // Set and Fill the weapon inventory by the childCount
        WeaponInventory = new GameObject[ArmsWeapons.transform.childCount];     
        for (int i = 0; i < ArmsWeapons.transform.childCount; i++)               
            WeaponInventory[i] = ArmsWeapons.transform.GetChild(i).gameObject;                                                                                                                                                         
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeaponByKey();
        ChangeWeaponByWheel();
    }

    //Change Weapon using number buttons
    void ChangeWeaponByKey()
    {
        if (Input.GetKey(KeyCode.Alpha1)) // index 0
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Knife))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Knife)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Knife].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2)) // index 1
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Pistol))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Pistol)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Pistol].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3)) // index 2
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.SMG))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.SMG)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.SMG].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha4)) // index 3
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Shotgun))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Shotgun)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Shotgun].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha5)) // index 4
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Rifle))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Rifle)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Rifle].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha6)) // index 5
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.LMG))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.LMG)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.LMG].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha7)) // index 6
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Crossbow))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Crossbow)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Crossbow].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha8)) // index 7
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.GrenadeLauncher))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.GrenadeLauncher)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.GrenadeLauncher].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha9)) // index 8
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory((int)Weapon.Axe))
            {
                if (GetActiveWeaponIndex() != (int)Weapon.Axe)
                {
                    DeactivateArms();
                    WeaponInventory[(int)Weapon.Axe].SetActive(true);
                }
            }
            else
            {
                Debug.Log("Weapon Not in Inventory");
            }
        }
    }
    void ChangeWeaponByWheel()
    {
        // Initialization
        /////////////////////////////////////////////////////////
        int curWeaponIndex = GetActiveWeaponIndex();
        int size = WeaponInventory.Length;
        int[] AvaileWeapons = new int[size];
        for (int x = 0; x < size; x++)
        {
            AvaileWeapons[x] = 0;
        }
        /////////////////////////////////////////////////////////

        for (int i = 1; i < WeaponInventory.Length; i++)
        {
            if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory(i))
            {
                AvaileWeapons[i] = i;
            }
            else
            {
                AvaileWeapons[i] = 0;
            }
        }

        // Check 1
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            int lastIndex = curWeaponIndex;

            for(int i = curWeaponIndex; i < AvaileWeapons.Length; i++)
            {
                if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory(i))
                    lastIndex++;
            }
            if (curWeaponIndex == lastIndex - 1)
            {
                for (int k = 0; k < AvaileWeapons.Length; k++)
                {
                    if (AvaileWeapons[k] != 0)
                    {
                        curWeaponIndex = k;
                        break;
                    }
                }
            }
            else
            {
                for (int j = curWeaponIndex + 1; j < AvaileWeapons.Length; j++)
                {
                    if (AvaileWeapons[j] != 0)
                    {
                        curWeaponIndex = j;
                        break;
                    }
                }
            }

            DeactivateArms();
            WeaponInventory[curWeaponIndex].SetActive(true);
        }

        // Check 2
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            int lastIndex = curWeaponIndex;

            for (int i = curWeaponIndex; i > 0; i--)
            {
                if (this.GetComponent<FPS_Inventory>().SearchWeaponInventory(i))
                    lastIndex--;
            }
            if (curWeaponIndex == lastIndex + 1)
            {
                for (int k = AvaileWeapons.Length - 1; k >= 1; k--)
                {
                    if (AvaileWeapons[k] != 0)
                    {
                        curWeaponIndex = k;
                        break;
                    }
                }
            }
            else
            {
                for (int j = curWeaponIndex - 1; j > 0; j--)
                {
                    if (AvaileWeapons[j] != 0)
                    {
                        curWeaponIndex = j;
                        break;
                    }
                }
            }

            DeactivateArms();
            WeaponInventory[curWeaponIndex].SetActive(true);
        }
    }
    void DeactivateArms()
    {
        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            WeaponInventory[i].SetActive(false);
        }
    }
    
    public void EquipPickedUpWeapon(int weaponIndex)
    {
        DeactivateArms();
        WeaponInventory[weaponIndex].SetActive(true);
    }
    public int GetActiveWeaponIndex()
    {
        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            if (WeaponInventory[i].activeInHierarchy == true)
                return i;
        }
        return 0;
    }
}
