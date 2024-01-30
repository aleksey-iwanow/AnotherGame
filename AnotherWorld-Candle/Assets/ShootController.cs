using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject gun;
    private Transform bulletSpawn;
    [SerializeField] private GameObject bullet;
    private float rotateZ;
    private float startTimeReload;
    private float timeReload;

    private void Start()
    {
        startTimeReload = 0.5f;
        bulletSpawn = gun.transform.GetChild(0);
    }

    private void Update()
    {
        if (gun.activeSelf)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(gun.transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            rotateZ = angle;
            if (Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x >= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rotateZ -= 180;
            }
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateZ));
        }
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && timeReload <= 0)
        {
            var gm = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            gm.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            gm.GetComponent<SpriteRenderer>().sortingOrder = gun.GetComponent<SpriteRenderer>().sortingOrder;
            gm.GetComponent<Bullet>().SetMoveVector(GameManager.convRad(rotateZ), transform.localScale.x);
            timeReload = startTimeReload;
        }
        else
        {
            timeReload -= Time.deltaTime;
        }
    }
}
