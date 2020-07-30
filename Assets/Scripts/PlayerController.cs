using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    bool jump = false;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        Vector2 vel = rb.velocity;
        vel.x = h * moveSpeed;
        rb.velocity = vel;

        if (!jump && Input.GetButtonDown("Jump"))
        { 
            rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            jump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        jump = false;
    }
}
