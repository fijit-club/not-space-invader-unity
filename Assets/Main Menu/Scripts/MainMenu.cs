using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayingState playingState;
    
    public void StartGame()
    {
        GameStateManager.ChangeState(playingState);
    }
}
