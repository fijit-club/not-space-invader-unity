using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : InGameComponents
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private DefaultShoot defaultShoot;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private LevelController levelController;
    
    public override void EnteredState()
    {
        playerRenderer.enabled = false;
        playerShooting.shooting = defaultShoot;
        levelController.ResetSpeed();
    }

    public override void LeftState()
    {
        playerRenderer.enabled = true;
    }
}
