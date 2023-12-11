using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private SelectShip selectShip;
    [SerializeField] private PlayerShooting playerShooting;
    
    public void TriggerAbility()
    {
        playerShooting.shooting = selectShip.currentSpaceship.shootingType;
    }

    public void DisableAbility()
    {
        
    }
}
