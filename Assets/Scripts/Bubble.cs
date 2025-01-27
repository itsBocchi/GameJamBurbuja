using UnityEngine;

public abstract class Bubble : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    protected Projectile projectile;

    public virtual void Burst()
    {
        projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.homingPlayer = true;
    }
}
