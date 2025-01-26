using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Tweakable variables
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float airborneAcceleration = 0.01f;
    [SerializeField] private bool acceleratedAirborneMovement = false;
    [SerializeField] private bool activateMomentum = false;

    // Private variables
    private PlayerAnimator animator;
    private Rigidbody2D rb;

    private Vector2 speedVector;
    private Vector2 momentum;
    private Vector2 externalSpeedVector = Vector2.zero;
    
    private float inputX;
    private float inputJump;
    private float xMovement;
    private float yMovement;
    private bool grounded = false;
    private bool jumped = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimator>();
    }

    /// <summary>
    /// Method that determines the vertical speed of the
    /// character.
    /// Speed is reduced when airborne.
    /// </summary>
    /// <returns>Vertical speed</returns>
    float HorizontalMovement()
    {
        if (grounded || !activateMomentum)
        {   // Movement is only based on horizontal input
            switch (inputX)
            {
                case > 0:
                    animator.Walk(PlayerAnimator.Direction.Right);
                    break;
                case < 0:
                    animator.Walk(PlayerAnimator.Direction.Left);
                    break;
                default:
                    animator.Idle();
                    break;
            }
            return inputX * speed;
        }
        
        else
        {   // Movement is based on momentum and input
            if (acceleratedAirborneMovement)
            {
                // Input translates into acceleration
                momentum.x += inputX * speed * airborneAcceleration;
                return momentum.x;
            }
            else
            {
                // Input translates into speed
                return momentum.x + inputX * speed;
            }
        }
    }

    /// <summary>
    /// Method that determines the vertical speed of the
    /// character.
    /// Only called when the character is grounded.
    /// </summary>
    /// <returns>Vertical speed</returns>
    float VerticalMovement()
    {
        if (inputJump > 0 && !jumped)
        {
            jumped = true;
        }
        if (grounded && jumped)
        {
            return Physics2D.gravity.y * -jumpHeight;
        }
        else if (grounded && !jumped)
        {
            return 0f;
        }
        else
        {
            return rb.velocity.y;
        }
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
        inputJump = Input.GetAxisRaw("Jump");
        inputX = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        xMovement = HorizontalMovement();
        yMovement = VerticalMovement();
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
        if (!n_grounded)
        {
            momentum = rb.velocity;
        }
        else
        {
            jumped = false;
        }
        grounded = n_grounded;
    }
}
