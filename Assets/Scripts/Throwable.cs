using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float throwForce = 20f;
    public float destroyTime = 2f;

    /*private void Start()
    {
        rb.velocity = transform.right * throwForce;
        Destroy(gameObject, destroyTime);
    }*/

    private void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (1.5f) * Time.deltaTime;
            //gameObject.transform.Rotate(0, 0, 10);
            gameObject.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hit");
            rb.velocity = Vector2.zero;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
           //Do nothing

    }

    private void OnEnable()
    {
        rb.velocity = transform.right * throwForce;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
