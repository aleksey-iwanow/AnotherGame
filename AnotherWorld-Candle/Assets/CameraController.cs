using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void FixedUpdate()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position+new Vector3(0,0.12f,0), new Vector3(player.position.x, player.position.y, transform.position.z), 0.06f);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, smoothPosition.z);
    }
}
