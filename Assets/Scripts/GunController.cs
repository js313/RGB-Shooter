using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun[] allGuns;

    Gun equippedGun;

    public void EquipGun(int weaponIndex) =>
      EquipGun(allGuns[weaponIndex]);

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun)
        {
            Destroy(equippedGun);
        }

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation, weaponHold.transform);
    }

    public void OnTriggerHold()
    {
        equippedGun?.OnTriggerHold();
    }

    public void OnTriggerRelease() => equippedGun?.OnTriggerRelease();
    public float GunHeight { get => 1f; }

    public void Aim(Vector3 aimPoint)
    {
        equippedGun?.Aim(aimPoint);
    }

    public void Reload() => equippedGun?.Reload();
}
