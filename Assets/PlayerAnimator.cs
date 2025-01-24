using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public enum Direction
    {
        Left = 0,
        Right = 1
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public void Walk(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                animator.SetTrigger("WalkL");
                break;
            case Direction.Right:
                animator.SetTrigger("WalkR");
                break;
            default:
                break;
        }
    }
}
