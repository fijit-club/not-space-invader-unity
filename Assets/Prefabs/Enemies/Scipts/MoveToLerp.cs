using UnityEngine;

public class MoveToLerp : MonoBehaviour
{
    [SerializeField] private Vector3 to;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, to, 10f * Time.deltaTime);
    }
}
