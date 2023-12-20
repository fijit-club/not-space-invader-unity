using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : InGameComponents
{
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private LevelController levelController;
    
    public override void EnteredState()
    {
        playerRenderer.enabled = false;
        levelController.ResetSpeed();
    }

    public override void LeftState()
    {
        playerRenderer.enabled = true;
    }
}
