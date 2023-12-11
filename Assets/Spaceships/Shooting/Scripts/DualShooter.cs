using UnityEngine;

public class DualShooter : ShootingType
{
    public override void Shoot()
    {
        CreateLaserShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
        guns.leftGunVFX.Play();
        CreateLaserShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
        guns.rightGunVFX.Play();
    }
}
