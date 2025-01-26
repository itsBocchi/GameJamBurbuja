using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private float baseSpeed;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float homingSpeedFactor;

    // Private variables
    private Rigidbody2D rb;
    private float angle;
    private PlayerMovement player;
    private Vector3 mousePos;
    private Vector3 playerPos;

    // Public variables
    public bool homingPlayer = false;

    // Called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = PlayerMovement.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (homingPlayer)
        {
            playerPos = player.transform.position;
            angle = Mathf.Atan2(transform.position.y - playerPos.y, transform.position.x - playerPos.x) * Mathf.Rad2Deg + 180;
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            angle = Mathf.Atan2(transform.position.y - mousePos.y, transform.position.x - mousePos.x) * Mathf.Rad2Deg + 180;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = transform.right * baseSpeed * homingSpeedFactor;
    }

    public void Expand()
    {
        Instantiate(bubblePrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (homingPlayer && collision.gameObject.tag == "Player")
        {
            PlayerShooting.Instance.RestoreBlood();
            Destroy(gameObject);
        }
        else if (!homingPlayer && collision.gameObject.tag == "Platform")
        {
            PlayerShooting.Instance.ProjectileHit();
            Expand();
        }
        else if (!homingPlayer && collision.gameObject.tag == "Bubblable")
        {
            PlayerShooting.Instance.ProjectileHit();
            collision.gameObject.GetComponent<BubbleInteractable>().BubbleInteraction();
            Destroy(gameObject);
        }
    }
}
