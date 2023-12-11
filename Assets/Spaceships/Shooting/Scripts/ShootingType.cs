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

    public abstract void Shoot();
}