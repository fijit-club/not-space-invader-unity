using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBullet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;
    
    
    private void Update()
    {
        transform.Translate(transform.InverseTransformDirection(Vector3.up * speed * Time.deltaTime)); 
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * 10f);
    }
}
