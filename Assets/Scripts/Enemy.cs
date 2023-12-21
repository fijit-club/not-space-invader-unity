using UnityEngine;

public class Enemy : EnemyMain
{
    
    #region FIELDS

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    #endregion

    public float speed;
    
    private void Start()
    {
        if (WaveData.WaveNumber > 10)
        {
            maxHealth = 3;
            health = 3;
        }
        Invoke("ActivateShooting", shotTimeMin);
    }

    //coroutine making a shot
    void ActivateShooting()
    {
        if (Random.Range(0, 3) == 1) return;
        var proj = Instantiate(Projectile, transform.position, Quaternion.identity, projectileParent);
        proj.GetComponent<DirectMoving>().speed = speed;
    }

    //method of getting damage for the 'Enemy'
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

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins++;
        Destroy(gameObject);
    }
}
