using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private Vector3 movement;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private ShootController shoot;

    private void Update() 
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector3.zero)
        {
            animator.SetBool("isWalk", true);
            if (!shoot.gun.activeSelf)
            {
                if (movement.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else if (movement.x > 0)
                    transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void LateUpdate()
    {
        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    private void Start()
    {
        shoot = GetComponent<ShootController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

}
