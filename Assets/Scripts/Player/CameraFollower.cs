using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Vector2 offset;


    Transform player;

    void Start()
    {
        player = PlayerMotor.player;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offset.y, player.position.z - offset.x);
        transform.LookAt(player, Camera.main.transform.up);
    }
}
