using UnityEngine;

public class DefaultShoot : ShootingType
{
    public override void Shoot()
    {
        CreateLaserShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        guns.centralGunVFX.Play();
    }
}