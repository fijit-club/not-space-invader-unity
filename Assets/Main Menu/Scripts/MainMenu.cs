using NotSpaceInvaders;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayingState playingState;
    [SerializeField] private SelectShip selectShip;
    
    public void StartGame()
    {
        Bridge.GetInstance().saveData.key[0] = "SELECTED_SHIP";
        Bridge.GetInstance().saveData.value[0] = selectShip.currentIndex;
        Bridge.GetInstance().SaveData();
        
        PlayerPrefs.SetInt("SELECTED_SHIP", selectShip.currentIndex);
        PlayerPrefs.Save();
        GameStateManager.ChangeState(playingState);
    }
}
