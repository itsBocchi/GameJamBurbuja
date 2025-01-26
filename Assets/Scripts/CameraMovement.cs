using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerMovement Player;
    
    void Start()
    {
        Player = PlayerMovement.Instance;
    }

    void Update()
    {
        var playerPos = Player.transform.position;
        gameObject.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
    }
}
