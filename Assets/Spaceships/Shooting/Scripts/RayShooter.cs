using System.Collections;
using UnityEngine;

public class RayShooter : ShootingType
{
    [SerializeField] private Transform player;
    [SerializeField] private float enableTime;
    
    private bool _shot;

    private GameObject _ray;
    
    public override void Shoot()
    {
        if (_shot) return;
        _shot = true;
        _ray = Instantiate(projectileObject, player.position, Quaternion.Euler(Vector3.zero), player);
    }

    public void DestroyRay()
    {
        if (_shot)
        {
            Destroy(_ray);
            _shot = false;
        }
    }
}
