using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Crossbow : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.Crossbow, 3);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.Crossbow);
            Destroy(this.gameObject);
        }
    }
}
