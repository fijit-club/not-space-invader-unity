using UnityEngine;

public class CircularShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float delay;
    
    private void Start()
    {
        delay = WaveData.CircularBossBulletDelay;
        InvokeRepeating(nameof(Spawn), .6f, delay);
    }

    private void Spawn()
    {
        var projInst = Instantiate(projectile, transform.position, transform.rotation);

        projInst.GetComponent<DirectMoving>().speed = WaveData.CircularBossBulletSpeed;
    }
}
