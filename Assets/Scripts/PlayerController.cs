using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpPower = 10f;

    [SerializeField] float attackTime = 1f; //攻撃判定が消えるまでの時間

    bool jump = false; //ジャンプの接地判定
    bool right; //右を向いているか
    bool left; //左を向いているか

    bool attackedUp = false;
    bool attackedRight = false;
    bool attackedLeft = false;

    [SerializeField] GameObject attackUp = default;
    [SerializeField] GameObject attackRight = default;
    [SerializeField] GameObject attackLeft = default;

    [SerializeField] Animator m_anim;

    Rigidbody2D m_rb;
    PhotonView m_view;
    void Start()
    {
        m_view = GetComponent<PhotonView>();

        attackUp = GameObject.Find("Attack_Up");
        attackRight = GameObject.Find("Attack_Right");
        attackLeft = GameObject.Find("Attack_Left");

        attackTime = 0f;

        if (m_view)
        {
            if (m_view.IsMine)
            {
                // 同期元（自分で操作して動かす）オブジェクトの場合のみ Rigidbody を使う
                m_rb = GetComponent<Rigidbody2D>();
            }
        }

        right = true;

        attackUp.SetActive(false);
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }
    void Update()
    {
        if (!m_view.IsMine) return;

        float h = Input.GetAxisRaw("Horizontal");

        if (m_anim)
        {
            Vector2 vel = m_rb.velocity;
            vel.x = h * moveSpeed;
            m_rb.velocity = vel;

            if (left)
            {
                m_anim.SetBool("Idle_left", true);

                if (vel.x < 0)
                {
                    m_anim.SetBool("Walk_left", true);
                }
                else
                {
                    m_anim.SetBool("Walk_left", false);
                }

                if (jump)
                {
                    m_anim.SetBool("Jump_left", true);
                }
                else
                {
                    m_anim.SetBool("Jump_left", false);
                }
            }
            else
            {
                m_anim.SetBool("Idle_left", false);
            }

            if (vel.x > 0)
            {
                m_anim.SetBool("Run", true);
            }
            else
            {
                m_anim.SetBool("Run", false);
            }

            if (jump)
            {
                m_anim.SetBool("Jump", true);
            }
            else
            {
                m_anim.SetBool("Jump", false);
            }
        }

        attackTime -= 0.1f;

        if (h > 0)
        {
            right = true;
            left = false;
        }
        else if (h < 0)
        {
            right = false;
            left = true;
        }

        if (!jump && Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            jump = true;
        }

        if (jump && Input.GetButtonDown("Fire1"))
        {
            m_view.RPC("AttackUp", RpcTarget.All);
        }
        if (right && Input.GetButtonDown("Fire1"))
        {
            if (m_anim)
            {
                m_anim.SetBool("Attack", true);
            }
            m_view.RPC("AttackRight", RpcTarget.All);
        }
        if (left && Input.GetButtonDown("Fire1"))
        {
            if (m_anim)
            {
                m_anim.SetBool("Attack_left", true);
            }
            m_view.RPC("AttackLeft", RpcTarget.All);
        }
        if (attackTime <= 0 && attackedUp)
        {
            m_view.RPC("AttackUpFinish", RpcTarget.All);
        }
        if (attackTime <= 0 && attackedRight)
        {
            if (m_anim)
            {
                m_anim.SetBool("Attack", false);
            }
            m_view.RPC("AttackRightFinish", RpcTarget.All);
        }
        if (attackTime <= 0 && attackedLeft)
        {
            if (m_anim)
            {
                m_anim.SetBool("Attack_left", false);
            }
            m_view.RPC("AttackLeftFinish", RpcTarget.All);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        jump = false;
    }

    [PunRPC]
    void AttackUp()
    {
        attackUp.SetActive(true);
        attackTime = 1f;
        attackedUp = true;
    }

    [PunRPC]
    void AttackRight()
    {
        attackRight.SetActive(true);
        attackTime = 1f;
        attackedRight = true;
    }

    [PunRPC]
    void AttackLeft()
    {
        attackLeft.SetActive(true);
        attackTime = 1f;
        attackedLeft = true;
    }

    [PunRPC]
    void AttackUpFinish()
    {
        attackUp.SetActive(false);
    }

    [PunRPC]
    void AttackRightFinish()
    {
        attackRight.SetActive(false);
    }

    [PunRPC]
    void AttackLeftFinish()
    {
        attackLeft.SetActive(false);
    }
}
