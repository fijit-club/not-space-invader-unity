using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSpinShooter : ShootingType
{
    public override void Shoot()
    {
        CreateLaserShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        guns.centralGunVFX.Play();
    }
}
