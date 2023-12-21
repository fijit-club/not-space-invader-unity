using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private SelectShip selectShip;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private DefaultShoot defaultShoot;
    [SerializeField] private float abilityTime;
    [SerializeField] private RayShooter rayShooter;
    
    public void TriggerAbility()
    {
        playerShooting.shooting = selectShip.currentSpaceship.shootingType;
        //buttonAnim.Play("Ability Button", -1, 0f);
        StartCoroutine(DisableAbility());
    }

    private IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(abilityTime);
        playerShooting.shooting = defaultShoot;
        rayShooter.DestroyRay();
    }
}
