using UnityEngine;

public class BubbleEntity : Bubble
{
    // Private variables
    private bool expanded = false;

    private void Awake()
    {
        PlayerShooting.Instance.SetActiveBubble(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bubblable")
        {
            collision.gameObject.GetComponent<BubbleInteractable>().BubbleInteraction();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && expanded)
        {
            collision.gameObject.GetComponent<PlayerMovement>().BubbleJump();
            Burst();
        }
    }

    public void BubbleExpanded()
    {
        expanded = true;
    }

    public override void Burst()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Burst");
    }

    public void BurstAftermath()
    {
        base.Burst();
        Destroy(gameObject);
    }
}
