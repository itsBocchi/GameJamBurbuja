using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float bubbleJumpHeight = 1f;
    [SerializeField] private float gargoyleJumpHeight = 2f;

    // Private variables
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Vector2 speedVector;
    [SerializeField] private Vector2 externalSpeedVector = Vector2.zero;

    [SerializeField] private float inputX;
    [SerializeField] private float inputJump;
    [SerializeField] private float xMovement;
    [SerializeField] private float yMovement;
    [SerializeField] private bool grounded = false;
    [SerializeField] private bool jumped = false;

    // Singleton instance
    [HideInInspector] public static PlayerMovement Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimator>();
    }

    /// <summary>
    /// Method that determines the vertical speed of the
    /// character.
    /// </summary>
    private void VerticalMovement()
    {
        inputJump = Input.GetAxisRaw("Jump");
        if (inputJump > 0 && !jumped)
        {
            jumped = true;
        }
        if (grounded && jumped)
        {
            Jump();
        }
        else if (grounded && !jumped)
        {
            yMovement = 0;
        }
        else if (!grounded)
        {
            yMovement = rb.velocity.y;
        }
        if (rb.velocity.y < 0)
        {
            Fall();
        }
    }

    private void Fall()
    {
        animator.Fall();
    }

    private void Jump(float boost=1f)
    {
        animator.Jump();
        yMovement = Physics2D.gravity.y * -jumpHeight * boost;
    }

    /// <summary>
    /// Method that determines the horizontal speed of the
    /// character.
    /// </summary>
    private void HorizontalMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (grounded && !jumped)
        {   // Determine animation from input
            if (Mathf.Abs(inputX) > 0)
            {
                animator.Walk();
            }
            else if (grounded)
            {
                animator.Idle();
            }
        }

        switch (inputX)
        {   // Determine animation direction
            case > 0:
                animator.DirectionCheck(PlayerAnimator.Direction.Right);
                break;
            case < 0:
                animator.DirectionCheck(PlayerAnimator.Direction.Left);
                break;
            default:
                break;
        }

        xMovement = inputX * speed;
    }

    /// <summary>
    /// <para>
    /// Public method that allows for external actors to
    /// apply movement vectors to the player.
    /// </para>
    /// 
    /// <list type="number">
    ///     <listheader>
    ///         <term>Use cases:</term>
    ///         <description>When should actors call this method.</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Moving platforms</term>
    ///         <description>Called in FixedUpdate when player is standing on platform.</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <param name="speedVector"></param>
    /// <returns></returns>
    public void ExternalMovement(Vector2 n_speed)
    {
        externalSpeedVector = n_speed;
    }

    // WIP
    public void BubbleJump()
    {
        Jump(bubbleJumpHeight);
        rb.velocity = new Vector2(rb.velocity.x, yMovement);
    }

    public void GargoyleJump()
    {
        Jump(gargoyleJumpHeight);
        rb.velocity = new Vector2(rb.velocity.x, yMovement);
    }


    /// <summary>
    /// Called once every 0.02 seconds.<br></br><br></br>
    /// Slightly delayed so as to allow ExternalMovement method to
    /// execute first.<br></br><br></br>
    /// Delay done in Project Settings -> Script Execution Order
    /// </summary>

    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
    }

    void FixedUpdate()
    {
        speedVector = new Vector2(xMovement, yMovement);

        // Applies and resets the vector of speed
        rb.velocity = speedVector + externalSpeedVector;
        externalSpeedVector = Vector2.zero;
    }

    /// <summary>
    /// Public method that allows for the change of the horizontal
    /// speed multiplier of the player.
    /// </summary>
    /// <param name="n_speed">New speed multiplier.</param>
    public void ChangeSpeed(float n_speed)
    {
        speed = n_speed;
    }

    /// <summary>
    /// Public method that allows the ground sensor to change
    /// wether the character is grounded or not.
    /// </summary>
    /// <param name="n_speed">New speed multiplier.</param>
    public void ChangeGrounded(bool n_grounded)
    {
        grounded = n_grounded;
        if (grounded)
        {
            jumped = false;
            animator.SetBool("TouchingGround", true);
        }
        else
        {
            animator.SetBool("TouchingGround", false);
        }
    }
}
