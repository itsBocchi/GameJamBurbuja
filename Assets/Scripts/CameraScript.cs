using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // find the game object with the tag "Player" and follow it
    // For camera2d
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector2 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)player.position + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }
}
