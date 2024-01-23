using NotSpaceInvaders;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayingState))]
public class BasicPlayingStateComponents : InGameComponents
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private TMP_Text[] highScoreText;
    
    
    public override void EnteredState()
    {
        foreach (var highScore in highScoreText)
            highScore.text = Bridge.GetInstance().thisPlayerInfo.highScore.ToString();

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
