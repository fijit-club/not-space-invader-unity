using UnityEngine;

public class PauseMenuControls : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    
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
}
