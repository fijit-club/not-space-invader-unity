using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBullet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    [SerializeField] private bool enemy;

    private void Start()
    {
        if (enemy)
            speed = WaveData.NormalBossBulletSpeed;
    }

    private void Update()
    {
        if (!enemy)
            transform.Translate(transform.InverseTransformDirection(Vector3.up * speed * Time.deltaTime));
        else
            transform.Translate(transform.InverseTransformDirection(-Vector3.up * speed * Time.deltaTime));
            
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * 10f);
    }
}
