using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : EnemyMain
{
    [SerializeField] private float rotationSpeed;

    private void OnDestroy()
    {
        var levelC = FindObjectOfType<LevelController>();
        if (!levelC) return;
        levelC.StartSpawning();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * 10f * Time.deltaTime);
    }

    public override void GetDamage(int damage)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }

    private void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins++;
        Destroy(gameObject);
    }
}
