using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;  // Cinemachine を使うため

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float nockBack = 10f;

    [SerializeField] float attackTime = 1f; //攻撃判定が消えるまでの時間
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] float mutekiJikan = 4; //無敵時間

    bool jump = false; //ジャンプの接地判定
    bool right; //右を向いているか
    bool left; //左を向いているか

    bool attackedUp = false;
    bool attackedRight = false;
    bool attackedLeft = false;
    bool damage = false;

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

        CinemachineVirtualCamera vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        if (m_view)
        {
            if (m_view.IsMine)
            {
                // 同期元（自分で操作して動かす）オブジェクトの場合のみ Rigidbody を使う
                m_rb = GetComponent<Rigidbody2D>();

                if (vCam)
                {
                    vCam.Follow = transform;
                }
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
        if (!damage)
        {
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
        //ダメージを受けたときの点滅判定
        if (damage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            renderer.color = new Color(1f, 1f, 1f, level);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!damage)
        {
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!damage)
        {
            //敵に当たったかどうかの判定
            if (collision.gameObject.tag == "enemy")
            {
                Debug.Log("敵に当たった");
                DamageEffect();
            }
        }
    }

    //敵に当たった時のどうするかの判定
    void DamageEffect()
    {
        // ダメージフラグON
        damage = true;

        // プレイヤーの位置を後ろに飛ばす
        float s = nockBack * Time.deltaTime;
        transform.Translate(Vector3.forward * s);

        // プレイヤーのlocalScaleでどちらを向いているのかを判定
        if (transform.localScale.x >= 0)
        {
            transform.Translate(Vector3.left * s);
        }
        else
        {
            transform.Translate(Vector3.right * s);
        }

        // コルーチン開始
        StartCoroutine("WaitForIt");
    }

    IEnumerator WaitForIt()
    {
        // 1秒間処理を止める
        yield return new WaitForSeconds(4);

        // １秒後ダメージフラグをfalseにして点滅を戻す
        damage = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Raise()
    {
        //イベントとして送るものを作る
        byte eventCode = 0; // イベントコード 0~199 まで指定できる。200 以上はシステムで使われているので使えない。
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,  // 全体に送る 他に MasterClient, Others が指定できる
        };  // イベントの起こし方
        SendOptions sendOptions = new SendOptions(); // オプションだが、特に何も指定しない
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        // イベントを起こす
        PhotonNetwork.RaiseEvent(eventCode, actorNumber, raiseEventOptions, sendOptions);
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
