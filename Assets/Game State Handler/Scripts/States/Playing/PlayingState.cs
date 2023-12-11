using UnityEngine;

public class PlayingState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] playingStateComponents;
    
    [SerializeField] private Animator abilityButton;
    
    public void OnEnter()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.EnteredState();
        if (abilityButton.gameObject.activeInHierarchy)
            abilityButton.Play("IDLE", -1, 0f);
    }

    public void StateUpdate()
    {
    }

    public void OnExit()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.LeftState();
    }
}
