using NotSpaceInvaders;
using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Transform projectileParent;
    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    public float projectileSpeed;
    
    public Transform healthBar;

    protected void ReduceHealth()
    {
        var scale = healthBar.localScale;
        scale.x -= 1f / maxHealth;
        if (scale.x > 0f)
            healthBar.localScale = scale;
    }
    
    public abstract void GetDamage(int damage, Vector3 position);
    protected void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        LevelController.Coins++;
        Destroy(gameObject);
        if (GameToggles.VibrationOn)
            Bridge.GetInstance().VibrateBridge(false);
    }
}