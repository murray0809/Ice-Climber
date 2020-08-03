using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpPower = 10f;

    [SerializeField] float attackTime = 1f;

    bool jump = false;

    [SerializeField] GameObject attack = default;

    Rigidbody2D m_rb;
    PhotonView m_view;
    void Start()
    {
        m_view = GetComponent<PhotonView>();

        attack = GameObject.Find("Attack");
        attackTime = 0f;

        if (m_view)
        {
            if (m_view.IsMine)
            {
                // 同期元（自分で操作して動かす）オブジェクトの場合のみ Rigidbody を使う
                m_rb = GetComponent<Rigidbody2D>();
            }
        }

        attack.SetActive(false);
    }
    void Update()
    {
        if (!m_view.IsMine) return;

        float h = Input.GetAxisRaw("Horizontal");
        
        Vector2 vel = m_rb.velocity;
        vel.x = h * moveSpeed;
        m_rb.velocity = vel;

        attackTime -= 0.1f;

        if (!jump && Input.GetButtonDown("Jump"))
        { 
            m_rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            attack.SetActive(true);
            attackTime = 1f;
            Debug.Log("攻撃した");
        }
        if (attackTime <= 0)
        {
            attack.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        jump = false;
    }
}
