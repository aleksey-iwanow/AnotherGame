using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSetter : MonoBehaviour
{
    public SpriteRenderer renderer;

    void Update()
    {
        if (renderer == null)
            return;

        GetComponent<ParticleSystemRenderer>().sortingOrder = renderer.sortingOrder;
    }
}
