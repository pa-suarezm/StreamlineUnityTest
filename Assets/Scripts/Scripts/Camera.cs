using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, ICamera
{
    private GameObject Player;

    [SerializeField] private float cameraSpeed = 1f;

    private void Update()
    {
        HandleInput();
        FollowPlayer();
    }

    private void HandleInput()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float current_rotation_y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, current_rotation_y + (mouse_x * cameraSpeed), 0));
    }

    private void FollowPlayer()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        Vector3 player_position = Player.transform.position;
        transform.position = new Vector3(player_position.x, player_position.y, player_position.z);
    }
}
