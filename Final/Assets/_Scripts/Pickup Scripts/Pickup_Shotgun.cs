using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Shotgun : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.Shotgun, 120);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.Shotgun);
            Destroy(this.gameObject);
        }
    }
}
