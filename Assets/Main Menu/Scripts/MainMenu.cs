using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayingState playingState;
    [SerializeField] private SelectShip selectShip;
    
    public void StartGame()
    {
        PlayerPrefs.SetInt("SELECTED_SHIP", selectShip.currentIndex);
        PlayerPrefs.Save();
        GameStateManager.ChangeState(playingState);
    }
}
