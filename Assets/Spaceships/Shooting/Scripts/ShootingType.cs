using UnityEngine;

public abstract class ShootingType : MonoBehaviour
{
    public Guns guns;
    public GameObject projectileObject;

    private void OnEnable()
    {
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    protected static void CreateLaserShot(GameObject laser, Vector3 pos, Vector3 rot)
    {
        Instantiate(laser, pos, Quaternion.Euler(rot));
    }

    public abstract void Shoot();
}