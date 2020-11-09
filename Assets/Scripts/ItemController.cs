using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private bool moving = false;

    /// <summary>弾の発射方向</summary>
    [SerializeField] Vector2 m_direction = Vector2.up;
    /// <summary>弾の飛ぶ速度</summary>
    [SerializeField] float m_bulletSpeed = 0.5f;
    Rigidbody2D m_rb2d;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        m_rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (moving)
        {
            //Vector3 v = m_direction.normalized * m_bulletSpeed;   // 弾が飛ぶ速度ベクトルを計算する
            //m_rb2d.velocity = v;                      // 速度ベクトルを弾にセットする

            Vector2 dir = player.transform.position - this.transform.position;
            dir = dir.normalized;

            // プレイヤーに向かって飛ばす
            m_rb2d.velocity = dir * m_bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("接触");
            moving = true;
        }
    }
}
