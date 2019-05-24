using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_LMG : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.LMG, 50);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.LMG);
            Destroy(this.gameObject);
        }
    }
}
