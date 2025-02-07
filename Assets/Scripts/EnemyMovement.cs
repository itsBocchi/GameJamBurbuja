using UnityEngine;

public class EnemyMovement : BubbleInteractable
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int currentWaypointIndex = 0;

    public bool isFlying = true;
    public bool isInBubble = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInBubble)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, speed * Time.deltaTime);
        
        if (!isInBubble)
        {
            if (currentWaypoint.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    public override void BubbleInteraction()
    {
        base.BubbleInteraction();
        isInBubble = true;
        animator.SetBool("InBubble", isInBubble);
    }

    public override void Burst()
    {
        base.Burst();
        isInBubble = false;
        animator.SetBool("InBubble", isInBubble);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isInBubble)
        {
            collision.gameObject.GetComponent<PlayerMovement>().GargoyleJump();
            Burst();
        }
    }
}
