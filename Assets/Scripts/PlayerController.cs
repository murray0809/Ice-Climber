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
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] float mutekiJikan = 4; //無敵時間

    bool jump = false; //ジャンプの接地判定
    float jumpAttackTime = 0;
   
    bool attackedUp = false;
    bool attackedRight = false;
    bool attackedLeft = false;
    bool damage = false;

    Vector3 scale;

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

        playerSprite = GetComponent<SpriteRenderer>(); 

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

        attackUp.SetActive(false);
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }
    void Update()
    {
        if (!m_view.IsMine) return;

        attackTime -= 0.1f;
        Debug.Log(jumpAttackTime);
        if (!damage)
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (m_anim)
            {
                if (h != 0)
                {
                    // キャラクターの向き制御
                    Vector2 lscale = gameObject.transform.localScale;
                    if ((lscale.x > 0 && h < 0)|| (lscale.x < 0 && h > 0))
                    {
                        lscale.x *= -1;
                        gameObject.transform.localScale = lscale;
                    }

                    // アニメーション切り替え（走っている）
                    m_anim.SetBool("Run", true);
                }
                else
                {
                    // アニメーション切り替え（立っている）
                    m_anim.SetBool("Run", false);
                }

                Vector2 vel = m_rb.velocity;
                vel.x = h * moveSpeed;
                m_rb.velocity = vel;
            }

            if (jump)
            {
                m_anim.SetBool("Jump", true);
                m_anim.SetBool("JumpAttack", true);
                jumpAttackTime -= 0.1f;
            }
            else if (jump && jumpAttackTime <= 0)
            {
                m_anim.SetBool("JumpAttack", false);
            }
            else
            {
                m_anim.SetBool("Jump", false);
            }


            if (!jump && Input.GetButtonDown("Jump"))
            {
                m_rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
                jump = true;
                jumpAttackTime = 2f;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (m_anim)
                {
                    m_anim.SetBool("Attack", true);
                }
                m_view.RPC("AttackRight", RpcTarget.All);
            }
            
            if (attackTime <= 0 && attackedRight)
            {
                if (m_anim)
                {
                    m_anim.SetBool("Attack", false);
                }
                m_view.RPC("AttackRightFinish", RpcTarget.All);
            }
        }
        //ダメージを受けたときの点滅判定
        if (damage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            playerSprite.color = new Color(1f, 1f, 1f, level);
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
        playerSprite.color = new Color(1f, 1f, 1f, 1f);
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
    void AttackRight()
    {
        attackRight.SetActive(true);
        attackTime = 1f;
        attackedRight = true;
    }

    [PunRPC]
    void AttackRightFinish()
    {
        attackRight.SetActive(false);
    }

    [PunRPC]
    void AttackTimeReset()
    {
        attackTime = 1f;
    }
}
