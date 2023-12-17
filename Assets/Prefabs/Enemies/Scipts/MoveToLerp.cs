using UnityEngine;

public class MoveToLerp : MonoBehaviour
{
    [SerializeField] private Vector3 to;
    
    private void Update()
    {
        to.x = transform.position.x;
        to.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, to, 10f * Time.deltaTime);
    }
}
