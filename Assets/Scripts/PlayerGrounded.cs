using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    // Private variables
    [SerializeField] private PlayerMovement movementScript;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            movementScript.ChangeGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            movementScript.ChangeGrounded(false);
        }
    }
}
