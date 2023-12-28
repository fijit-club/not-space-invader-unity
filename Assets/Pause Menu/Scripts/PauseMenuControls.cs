using NotSpaceInvaders;
using UnityEngine;

public class PauseMenuControls : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private MainMenuState mainMenuState;
    [SerializeField] private LevelController levelController;
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void SubmitAndExit()
    {
        Bridge.GetInstance().coinsCollected = levelController.coins;
        Bridge.GetInstance().SendScore(levelController.score);
        ResumeGame();
        GameStateManager.ChangeState(mainMenuState);
    }
}
