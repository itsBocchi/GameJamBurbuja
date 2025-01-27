using System;
using UnityEngine;

public class Basket : BubbleInteractable
{
    [SerializeField] private float liftSpeed;

    public PlayerMovement player;
    private bool flying = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flying)
        {
            rb.velocity = new Vector2(0, liftSpeed);
        }

        if (player != null)
        {
            player.ExternalMovement(rb.velocity);
        }
    }

    public override void Burst()
    {
        base.Burst();
        gameObject.GetComponent<Animator>().SetTrigger("Fall");
        flying = false;
        gameObject.tag = "Bubblable";
    }

    public override void BubbleInteraction()
    {
        base.BubbleInteraction();
        gameObject.GetComponent<Animator>().SetTrigger("Lift");
        gameObject.tag = "Platform";
    }

    public void StartLift()
    {
        flying = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundSensor")
        {
            player = PlayerMovement.Instance;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Console.WriteLine(collision.name);
        if (collision.gameObject.tag == "GroundSensor")
        {
            player = null;
        }
    }
}
