using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    [SerializeField] private float abilitySpawnTime = 10f;
    [SerializeField] private GameObject abilityPoint;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private Transform transformParent;

    public void StartSpawning()
    {
        if (gameObject.activeInHierarchy)
            InvokeRepeating(nameof(SpawnAbility), abilitySpawnTime, abilitySpawnTime);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnAbility));
    }

    private void SpawnAbility()
    {
        var position = transform.position;
        var pos = new Vector3(Random.Range(xMin, xMax), position.y, position.z);
        Instantiate(abilityPoint, pos, Quaternion.identity, transformParent);
    }
}
