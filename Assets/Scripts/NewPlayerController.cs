using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpPower = 10f;

    [SerializeField] SpriteRenderer playerSprite;

    [SerializeField] Animator m_anim;

    Rigidbody2D m_rb;
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (!m_anim)
        {
            if (h < 0)
            {
                playerSprite.flipX = true;
            }
            if(h > 0)
            {
                playerSprite.flipX = false;
            }
            Vector2 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;
        }
    }
}
