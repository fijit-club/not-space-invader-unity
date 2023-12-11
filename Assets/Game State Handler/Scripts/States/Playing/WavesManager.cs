using UnityEngine;

public class WavesManager : InGameComponents
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private Transform enemyParent;
    
    public override void EnteredState()
    {
        levelController.StartWaves();
    }

    public override void LeftState()
    {
        for (int i = 0; i < enemyParent.childCount; i++)
            Destroy(enemyParent.GetChild(i).gameObject);
            
        if (FindObjectOfType<Wave>())
            FindObjectOfType<Wave>().StopAllCoroutines();
    }
}
