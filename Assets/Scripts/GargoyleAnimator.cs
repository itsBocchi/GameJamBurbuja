using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        // set the animator component
        animator = GetComponent<Animator>();
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void Flying(bool value)
    {
        animator.SetBool("Flying", value);
    }

    public void OnBubble(bool value)
    {
        animator.SetBool("InBubble", value);
    }
}
