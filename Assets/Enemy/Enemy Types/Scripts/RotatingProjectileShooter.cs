using UnityEngine;

public class RotatingProjectileShooter : EnemyMain
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float delay;
    [SerializeField] private float delayOffset;
    [SerializeField] private bool dualShooter;
    
    private float _initialX;
    
    private void Start()
    {
        _initialX = transform.position.x;
        delay = WaveData.NormalBossBulletDelay;
        InvokeRepeating(nameof(Spawn), .2f, delay + delayOffset);
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

    public override void GetDamage(int damage, Vector3 position)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
        LevelController.Score += 10;
        
        ReduceHealth();
    }

    private void OnDestroy()
    {
        if (!dualShooter)
        {
            var levelC1 = FindObjectOfType<LevelController>();
            if (!levelC1) return;
            levelC1.StartSpawning();
            
            return;
        }
        var levelC = FindObjectOfType<LevelController>();
        if (!levelC) return;
        levelC.wavingIndex++;
        if (levelC.wavingIndex == 3)
            levelC.StartSpawning();
    }
    
    private void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins += 5;
        Destroy(gameObject);
    }
}
