using UnityEngine;

[RequireComponent(typeof(MainMenuState))]
public class BasicMainMenuComponents : InGameComponents
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private SelectShip selectShip;
    
    private bool _gameStarted;
    private Vector3 _playerInitLocation;
    
    public override void EnteredState()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
            _playerInitLocation = player.transform.position;
        }
        
        selectShip.UpdateProperties();
        
        player.SetActive(true);
        player.transform.position = _playerInitLocation;

        foreach (var go in gameObjectsToEnable)
            go.SetActive(true);

        foreach (var component in componentsToEnable)
            component.enabled = true;

        foreach (var go in gameObjectsToDisable)
            go.SetActive(false);

        foreach (var component in componentsToDisable)
            component.enabled = false;
    }

    public override void LeftState()
    {
        foreach (var go in gameObjectsToEnable)
            go.SetActive(false);
        
        foreach (var component in componentsToEnable)
            component.enabled = false;
    }
}