using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_SMG : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Inventory>().AddWeaponToInventory((int)Weapon.SMG, 30);
            other.gameObject.GetComponent<FPS_WeaponHandling>().EquipPickedUpWeapon((int)Weapon.SMG);
            Destroy(this.gameObject);
        }
    }
}
