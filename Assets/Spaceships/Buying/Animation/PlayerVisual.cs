using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SelectShip selectShip;

    [SerializeField] private Button[] buttons;
    [SerializeField] private Image image;
    
    public void DisableButtons()
    { 
        image.enabled = true;
        
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    public void EnableButtons()
    {
        image.enabled = false;
        
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
    
    public void Change()
    {
        selectShip.ChangeSprite();
    }
}
