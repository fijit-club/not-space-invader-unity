﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves the attached object along the Y-axis with the defined speed
/// </summary>
public class DirectMoving : MonoBehaviour {

    [Tooltip("Moving speed on Y axis in local space")]
    public float speed;

    [SerializeField] private bool followPlayer = true;
    
    public bool enemy;

    private Transform _player;

    private float _time;

    private void Start()
    {
        if (!enemy || !followPlayer) return;
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 lookAtPos = transform.position;

        if (WaveData.WaveNumber < 10)
        {
            float rand = Random.Range(-1f, 1f);
            if (rand >= 0)
                rand += 1f;
            else
                rand -= 1f;
            lookAtPos = new Vector3(transform.position.x + rand, transform.position.y + rand, transform.position.z);
        }
        
        Vector3 difference = _player.position - lookAtPos;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    //moving the object with the defined speed
    private void Update()
    {
        if (enemy && followPlayer)
        {
            transform.Translate(transform.InverseTransformDirection(transform.right * speed * Time.deltaTime)); 
            
            return;
        }

        if (enemy)
        {
            transform.Translate(transform.InverseTransformDirection(-transform.up * speed * Time.deltaTime));
            return;
        }
        transform.Translate(transform.up * speed * Time.deltaTime); 
    }
}
