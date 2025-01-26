using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Private variables
    [SerializeField] private Transform shooter;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    private Quaternion rotate;
    private bool shooting = false;
    private Vector3 mousePos;
    private float angle;

    // Called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && shooting)
        {
            projectile.Expand();
            shooting = false;
        }
        if (Input.GetButtonDown("Fire1") && !shooting)
        {
            // Rotation is extracted from parent
            // The 'z' angle compensates for the sprite's rotation
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            angle = Mathf.Atan2(transform.position.y - mousePos.y, transform.position.x - mousePos.x) * Mathf.Rad2Deg + 180;
            rotate = Quaternion.Euler(new Vector3(0, 0, angle));
            projectile = Instantiate(projectilePrefab, shooter.transform.position, rotate).GetComponent<Projectile>();
            shooting = true;
        }
    }
}
