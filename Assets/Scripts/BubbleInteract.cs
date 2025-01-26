using UnityEngine;

public class BubbleInteract : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private GameObject projectilePrefab;

    // Private variables
    private Projectile projectile;
    private bool expanded = false;

    private void Awake()
    {
        PlayerShooting.Instance.SetActiveBubble(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bubblable")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<BubbleInteractable>().BubbleInteraction();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && expanded)
        {
            collision.gameObject.GetComponent<PlayerMovement>().BubbleJump();
            Burst();
        }
    }

    public void BubbleExpanded()
    {
        expanded = true;
    }

    public void Burst()
    {
        Destroy(gameObject);
        projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.homingPlayer = true;
    }
}
