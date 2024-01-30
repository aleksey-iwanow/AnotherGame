using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Camera camera;


    private void Update()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position+new Vector3(0,0.12f,0), new Vector3(player.position.x, player.position.y, transform.position.z), 0.06f);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, smoothPosition.z);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0 && camera.orthographicSize < 10)
        {
            camera.orthographicSize += 0.2f;
        }
        else if (scroll > 0 && camera.orthographicSize > 5)
        {
            camera.orthographicSize -= 0.2f;
        }
    }
}
