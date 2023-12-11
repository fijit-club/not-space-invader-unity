using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProperties : InGameComponents
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private DefaultShoot defaultShoot;
    
    
    public override void EnteredState()
    {
        playerShooting.shooting = defaultShoot;
    }

    public override void LeftState()
    {
    }
}
