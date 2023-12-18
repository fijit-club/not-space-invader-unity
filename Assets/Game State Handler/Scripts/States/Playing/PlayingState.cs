using UnityEngine;

public class PlayingState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] playingStateComponents;
    [SerializeField] private AbilitySpawner abilitySpawner;
    
    public void OnEnter()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.EnteredState();
        
        abilitySpawner.StartSpawning();
    }

    public void StateUpdate()
    {
    }

    public void OnExit()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.LeftState();
        
        abilitySpawner.StopSpawning();
    }
}
