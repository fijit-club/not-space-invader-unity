using UnityEngine;

public class DefaultShoot : ShootingType
{
    public override void Shoot()
    {
        CreateLaserShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        guns.centralGunVFX.Play();
    }

    private static void CreateLaserShot(GameObject laser, Vector3 pos, Vector3 rot)
    {
        Instantiate(laser, pos, Quaternion.Euler(rot));
    }
}