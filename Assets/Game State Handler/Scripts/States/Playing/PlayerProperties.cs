using UnityEngine;

public class PlayerProperties : InGameComponents
{
    [SerializeField] private PlayerShooting playerShooting;

    public override void EnteredState()
    {
        playerShooting.weaponPower = 1;
    }

    public override void LeftState()
    {
    }
}
