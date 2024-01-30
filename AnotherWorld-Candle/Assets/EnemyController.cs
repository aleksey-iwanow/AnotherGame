using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;
    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;
    private OrderController orderController;
    public int health;
    [SerializeField] private GameObject particleDead;
    private bool isDead;
    private bool isLess;
    [HideInInspector] public string type_;
    [SerializeField] public float attackDistance;

    IEnumerator LessColor()
    {
        ChangeColor(new Color(0.75f, 0.75f, 0.75f, 1f));
        yield return new WaitForSeconds(0.4f);
        ChangeColor(Color.white);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
        orderController = GetComponent<OrderController>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        type_ = "Attacker";
    }

    public void Dead()
    {
        Instantiate(particleDead, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    public void LessHealth()
    {
        isLess = true;
        StopCoroutine(LessColor());
        health -= 1;
        if (health > 0)
            StartCoroutine(LessColor());
        else
        {
            isDead = true;
            animator.SetTrigger("dead");
        }
    }

    private void ChangeColor(Color color)
    {
        for (int i = 0; i < orderController.renderers.Length; i++)
        {
            orderController.renderers[i].color = color;
        }
    }

    void Update()
    {
        if (isDead)
        {
            agent.SetDestination(transform.position);
            return;
        }
            
        float distance = Vector2.Distance(target.position, transform.position);
        if (type_ == "Attacker")
        {
            if (distance >= attackDistance && !isLess) {
                agent.SetDestination(transform.position);
            }
            else if(distance > 0.85f)
                agent.SetDestination(target.position);
            else
                agent.SetDestination(transform.position);
        }

        if (agent.velocity != Vector3.zero)
        {
            animator.SetBool("isWalk", true);
            if (agent.velocity.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
}
