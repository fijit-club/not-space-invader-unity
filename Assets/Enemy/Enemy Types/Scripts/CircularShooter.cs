using UnityEngine;

public class CircularShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float delay;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), .6f, delay);
    }

    private void Spawn()
    {
        var projInst = Instantiate(projectile, transform.position, transform.rotation);
    }
}
