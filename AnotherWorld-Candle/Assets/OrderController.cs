using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    private Transform target;
    private List<int> defaultsOrder = new List<int>();
    [SerializeField] private float step;
    [SerializeField] private SpriteRenderer[] renderers;
    void Start()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            defaultsOrder.Add(renderers[i].sortingOrder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        if (target.position.y > transform.position.y - step)
            SetDefault();
        else
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sortingOrder = target.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder - 1;
            }

    }

    private void SetDefault()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].sortingOrder = defaultsOrder[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collisionObject")
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == target.name)
        {
            target = null;
            SetDefault();
        }
    }
}
