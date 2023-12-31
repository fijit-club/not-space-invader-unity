﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    public bool ray;

    private float _time;
    [SerializeField] private float damageTimeRay;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        _time += Time.deltaTime;
        if (other.CompareTag("Enemy") && !enemyBullet && ray && _time > damageTimeRay)
        {
            print("TOOK DAMAGE");
            _time = 0f;
            other.GetComponent<EnemyMain>().GetDamage(damage, other.ClosestPoint(transform.position));
            if (destroyedByCollision)
                Destruction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (ray) return;
        if (enemyBullet && collision.CompareTag("Player")) //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyBullet && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyMain>().GetDamage(damage, collision.ClosestPoint(transform.position));
            if (destroyedByCollision)
                Destruction();
                
        }
    }

    void Destruction()
    {
        if (ray) return;
        Destroy(gameObject);
    }
}


