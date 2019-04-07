using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPS_Inventory : MonoBehaviour
{
    public enum Weapons { Knife, Pistol, SMG, Shotgun, Rifle, LMG, Crossbow, GrenadeLauncher, Axe, Grenade, Flashlight };
    private enum Ammo { LightAmmo, HeavyAmmo, Shells, Bolts };
    private enum Consumables { Syringe };
    private enum KeyItems { CardKey };

    [Serializable]
    struct Weapon
    {
        public bool obtained;
        public int ammo;

        public Weapon(bool _obtained, int _ammo)
        {
            obtained = _obtained;
            ammo = _ammo;
        }
    }

    #region Variables
    [SerializeField]
    private Weapon[] WeaponInventory;
    [SerializeField]
    private bool[] ConsumableInventory;
    [SerializeField]
    private bool[] KeyItemInventory;
    #endregion Variables



    // Initiliaze our different Inventory Item Arrays
    void Start()
    {
        WeaponInventory = new Weapon[11];
        ConsumableInventory = new bool[1];
        KeyItemInventory = new bool[1];

        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            WeaponInventory[i].obtained = false;
            WeaponInventory[i].ammo = 0;
        }

        for (int i = 0; i < ConsumableInventory.Length; i++)
        {
            ConsumableInventory[i] = false;
        }
        for (int i = 0; i < KeyItemInventory.Length; i++)
        {
            KeyItemInventory[i] = false;
        }
        // Set our starting items here(For Testing)
        SetStartingItems();
    }


    // Update is called once per frame
    void Update()
    {

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// Helper Methods 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Called by WeaponHandling: 
    // Searches the player Weapon Inventory, returns true if weapon is obstained, false if not obtained
    public bool SearchWeaponInventory(int weaponIndex)
    {
        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            if (i == weaponIndex && WeaponInventory[i].obtained == true)
            {
                return true;
            }
        }
        return false;
    }

    // Called in Start:
    // Gives the players a few items to start with
    void SetStartingItems()
    {
        for (int i = 0; i < WeaponInventory.Length; i++)
        {
            WeaponInventory[i].obtained = false;
        }
        WeaponInventory[(int)Weapons.Knife].obtained = true;
    }

    public void AddWeaponToInventory(int weaponIndex, int _ammo)
    {
        WeaponInventory[weaponIndex].obtained = true;
        WeaponInventory[weaponIndex].ammo += _ammo;
    }

    public int GetWeaponAmmo(int weaponIndex)
    {
        return WeaponInventory[weaponIndex].ammo;
    }

    public void ModifyWeaponAmmo(int weaponIndex, string operation, int amnt)
    {
        switch(operation)
        {
            case "add":
                {
                    WeaponInventory[weaponIndex].ammo += amnt;
                    break;
                }
            case "sub":
                {
                    WeaponInventory[weaponIndex].ammo -= amnt;
                    break;
                }
        }
    }
}
