using UnityEngine;

public class TripleShooting : ShootingType
{
    public override void Shoot()
    {
        CreateLaserShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        CreateLaserShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
        guns.leftGunVFX.Play();
        shoot.Play();
        CreateLaserShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
        guns.rightGunVFX.Play();
    }
}
