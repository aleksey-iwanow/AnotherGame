using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    private Transform target;
    private List<int> defaultsOrder = new List<int>();
    private bool stay;
    [HideInInspector] public int orderLayer;
    [SerializeField] private float step;
    [SerializeField] private string exceptionTag;
    [SerializeField] public SpriteRenderer[] renderers;

    void Start()
    {
        orderLayer = renderers[0].sortingOrder;
        for (int i = 0; i < renderers.Length; i++)
        {
            defaultsOrder.Add(renderers[i].sortingOrder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stay = false;
        if (target == null)
            return;
        if (target.position.y > transform.position.y - step)
            SetDefault();
        else
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sortingOrder = target.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 3;
            }
            orderLayer = renderers[0].sortingOrder;
        }
    }

    private void SetDefault()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sortingOrder = defaultsOrder[i];
        }
        orderLayer = renderers[0].sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collisionObject" && collision.tag != exceptionTag)
        {
            target = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "collisionObject")
        {
            stay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target != null && collision.name == target.name && !stay)
        {
            target = null;
            SetDefault();
        }
    }
}
