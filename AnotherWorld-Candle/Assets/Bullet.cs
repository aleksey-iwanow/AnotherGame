using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveV;
    private float scaleX;
    private float speed = 10;
    [SerializeField] private GameObject particle;

    IEnumerator deadTick()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void SetMoveVector(float angle, float _scaleX)
    {
        StartCoroutine(deadTick());
        scaleX = _scaleX;
        moveV = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    void Update()
    {
        if (moveV == Vector3.zero)
            return;

        transform.position += moveV * Time.deltaTime * speed * scaleX;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision.isTrigger)
        {
            var par = Instantiate(particle, transform.position - moveV*0.2f, Quaternion.identity, collision.transform);
            par.GetComponent<ParticleSystemRenderer>().sortingOrder = collision.GetComponent<OrderController>().orderLayer + 1;
            collision.GetComponent<EnemyController>().LessHealth();
            Destroy(gameObject);
        }
    }
}
