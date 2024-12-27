using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setUpBullet(Vector2 _movement)
    {
        this.movement = _movement;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
