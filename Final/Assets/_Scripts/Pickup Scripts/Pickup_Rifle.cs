using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Rifle : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.Rifle, 4);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.Rifle);
            Destroy(this.gameObject);
        }
    }
}
