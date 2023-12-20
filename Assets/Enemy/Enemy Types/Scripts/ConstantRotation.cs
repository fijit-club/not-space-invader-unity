using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : EnemyMain
{
    [SerializeField] private float rotationSpeed;

    private Vector3 _initPosition;
    
    private void Start()
    {
        _initPosition = healthBar.position - transform.position;
        healthBar.parent = null;
    }

    private void OnDestroy()
    {
        Destroy(healthBar.gameObject);
        
        var levelC = FindObjectOfType<LevelController>();
        if (!levelC) return;
        levelC.StartSpawning();
    }

    private void Update()
    {
        healthBar.position = transform.position + _initPosition;
        transform.Rotate(0f, 0f, rotationSpeed * 10f * Time.deltaTime);
    }

    public override void GetDamage(int damage, Vector3 position)
    {
        health -= damage;
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect, position,Quaternion.identity);
        LevelController.Score += 10;
        
        ReduceHealth();
    }

    private void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins += 5;
        Destroy(gameObject);
    }
}
