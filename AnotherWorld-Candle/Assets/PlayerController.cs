using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private Vector3 movement;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private ShootController shoot;
    [HideInInspector] public int health;
    private CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    IEnumerator PerlinOff()
    {
        yield return new WaitForSeconds(1);
        channelPerlin.m_FrequencyGain = 0;
    }

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

    public void LessHealth()
    {
        health -= 1;
        StopCoroutine(PerlinOff());
        channelPerlin.m_FrequencyGain = 1;
        StartCoroutine(PerlinOff());
    }

    private void Start()
    {
        health = 25;
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        channelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shoot = GetComponent<ShootController>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

}
