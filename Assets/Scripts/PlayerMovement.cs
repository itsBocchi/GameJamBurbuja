using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpHeight = 0.5f;

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
    /// <returns>Vertical speed</returns>
    float VerticalMovement()
    {
        if (inputJump > 0 && !jumped)
        {
            jumped = true;
            animator.Jump();
        }
        if (grounded && jumped)
        {
            return Physics2D.gravity.y * -jumpHeight;
        }
        else
        {
            return rb.velocity.y;
        }
    }

    /// <summary>
    /// Method that determines the horizontal speed of the
    /// character.
    /// </summary>
    /// <returns>Horizontal speed</returns>
    float HorizontalMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (grounded && !jumped)
        {   // Determine animation from input
            if (Mathf.Abs(inputX) > 0)
            {
                animator.Walk();
            }
            else
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

        return inputX * speed;
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
    public void ExternalMovement(Vector2 addedSpeed)
    {
        externalSpeedVector += addedSpeed;
    }

    // WIP
    Vector2 BubbleJump()
    {
        return Vector2.zero;
    }

    /// <summary>
    /// Called once every 0.02 seconds.<br></br><br></br>
    /// Slightly delayed so as to allow ExternalMovement method to
    /// execute first.<br></br><br></br>
    /// Delay done in Project Settings -> Script Execution Order
    /// </summary>

    void Update()
    {
        yMovement = VerticalMovement();
        xMovement = HorizontalMovement();
        yMovement = VerticalMovement();
        inputJump = Input.GetAxisRaw("Jump");
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
        if (n_grounded)
        {
            jumped = false;
            animator.Idle();
        }
        grounded = n_grounded;
    }
}
