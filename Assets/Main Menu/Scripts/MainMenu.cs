using NotSpaceInvaders;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayingState playingState;
    [SerializeField] private SelectShip selectShip;
    
    public void StartGame()
    {
        Bridge.GetInstance().SaveData(selectShip.currentIndex);
        GameStateManager.ChangeState(playingState);
    }
}
