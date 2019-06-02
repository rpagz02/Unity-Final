using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_GrenadeLauncher : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.GrenadeLauncher, 4);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.GrenadeLauncher);
            Destroy(this.gameObject);
        }
    }
}
