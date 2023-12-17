using UnityEngine;

public class RotatingProjectileShooter : EnemyMain
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private GameObject projectile;

    private float _initialX;
    
    private void Start()
    {
        _initialX = transform.position.x;
        InvokeRepeating(nameof(Spawn), .2f, .7f);
    }
    
    private void Update()
    {
        float x = _initialX + amplitude * Mathf.Sin(Time.time * speed);

        var enemyTransform = transform;
        var pos = enemyTransform.position;
        
        pos = new Vector3(x, pos.y, pos.z);
        enemyTransform.position = pos;
    }

    private void Spawn()
    {
        Instantiate(projectile, transform.position, Quaternion.identity, projectileParent);
    }

    public override void GetDamage(int damage)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }

    private void OnDestroy()
    {
        var levelC = FindObjectOfType<LevelController>();
        if (!levelC) return;
        levelC.StartSpawning();
    }
    
    private void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins++;
        Destroy(gameObject);
    }
}
