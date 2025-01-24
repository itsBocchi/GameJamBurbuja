using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Private variables
    [SerializeField] private Vector2 speedVector;
    [SerializeField] private Vector2 momentum;
    [SerializeField] private Vector2 deltaSpeedVector = Vector2.zero;
    [SerializeField] private PlayerAnimator animator;

    [SerializeField] private float speed = 4f;
    private Rigidbody2D rb;
    private float inputX;
    private float inputJump;
    [SerializeField] private bool grounded = false;
    private float xMovement;
    private float yMovement;

    //airborne = momentum + horizontal movement
    //grounded = external forces + horizontal + vertical

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Method that determines the vertical speed of the
    /// character.
    /// Speed is reduced when airborne.
    /// </summary>
    /// <returns>Vertical speed</returns>
    float HorizontalMovement()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (grounded)
        {
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
        {
            return inputX * speed * 0.5f;
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
        inputJump = Input.GetAxisRaw("Jump");
        if (inputJump > 0 && grounded)
        {
            return Physics2D.gravity.y * -0.5f;
        }
        else if (grounded)
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
        deltaSpeedVector += addedSpeed;
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
    void FixedUpdate()
    {
        xMovement = HorizontalMovement();
        yMovement = VerticalMovement();
        if (grounded)
        {
            deltaSpeedVector = Vector2.zero;
        }
        else
        {
            momentum = deltaSpeedVector;
        }

        // Applies and resets the vector of speed
        deltaSpeedVector = Vector2.zero;
        rb.velocity = speedVector;
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
    }
}
