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
            Destroy(gameObject);
            collision.gameObject.GetComponent<BubbleInteractable>().BubbleInteraction();
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
        base.Burst();
        Destroy(gameObject);
    }
}
