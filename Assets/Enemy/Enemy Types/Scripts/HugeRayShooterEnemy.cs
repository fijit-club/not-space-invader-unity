using UnityEngine;

public class HugeRayShooterEnemy : EnemyMain
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private Animator projectile;
    [SerializeField] private float projectileSpawnDelay = 7f;
    [SerializeField] private float projectileSpawnStartTime = 2f;
    
    private float _initialX;
    private void Start()
    {
        _initialX = transform.position.x;
        
        InvokeRepeating(nameof(SpawnRay), projectileSpawnStartTime, projectileSpawnDelay);
    }

    private void SpawnRay()
    {
        if (Random.Range(0, 2) != 1)
            projectile.Play("SpawnRay", -1, 0f);
    }
    
    private void Update()
    {
        float x = _initialX + amplitude * Mathf.Sin(Time.time * speed);

        var enemyTransform = transform;
        var pos = enemyTransform.position;
        
        pos = new Vector3(x, pos.y, pos.z);
        enemyTransform.position = pos;
    }

    public override void GetDamage(int damage, Vector3 position)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
        LevelController.Score += 10;
    }

    private void OnDestroy()
    {
        var levelC = FindObjectOfType<LevelController>();
        if (!levelC) return;
        levelC.wavingIndex++;
        if (levelC.wavingIndex == 2)
            levelC.StartSpawning();
            
    }
}
