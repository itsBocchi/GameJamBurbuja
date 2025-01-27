public abstract class BubbleInteractable : Bubble
{
    public virtual void BubbleInteraction()
    {
        PlayerShooting.Instance.SetActiveBubble(this);
    }
}
