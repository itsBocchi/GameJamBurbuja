using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private GameObject bubblePrefab;

    // Private variables
    private Rigidbody2D rb;

    // Called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * baseSpeed;
    }

    public void Expand()
    {
        Instantiate(bubblePrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
