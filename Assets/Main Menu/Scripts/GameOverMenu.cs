using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private MainMenuState mainMenuState;

    public void Replay()
    {
        GameStateManager.ChangeState(mainMenuState);
    }
}
