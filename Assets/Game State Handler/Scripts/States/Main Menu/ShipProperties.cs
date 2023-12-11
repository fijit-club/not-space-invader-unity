using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : InGameComponents
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private DefaultShoot defaultShoot;
    [SerializeField] private SpriteRenderer playerRenderer;
    
    public override void EnteredState()
    {
        playerRenderer.enabled = false;
        playerShooting.shooting = defaultShoot;
    }

    public override void LeftState()
    {
        playerRenderer.enabled = true;
    }
}
