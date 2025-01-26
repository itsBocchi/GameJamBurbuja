using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public enum Direction
    {
        Left = 0,
        Right = 1
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public void Walk()
    {
        animator.SetTrigger("Walk");
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void DirectionCheck(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                animator.SetTrigger("Left");
                break;
            case Direction.Right:
                animator.SetTrigger("Right");
                break;
            default:
                break;
        }
    }
}
