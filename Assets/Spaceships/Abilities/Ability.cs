using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private SelectShip selectShip;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private DefaultShoot defaultShoot;
    [SerializeField] private float abilityTime;
    [SerializeField] private Animator buttonAnim;
    [SerializeField] private RayShooter rayShooter;
    
    public void TriggerAbility()
    {
        playerShooting.shooting = selectShip.currentSpaceship.shootingType;
        buttonAnim.Play("Ability Button", -1, 0f);
    }

    public void DisableAbility()
    {
        playerShooting.shooting = defaultShoot;
        rayShooter.DestroyRay();
    }
}
