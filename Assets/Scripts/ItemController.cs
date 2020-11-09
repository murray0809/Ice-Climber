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

    [SerializeField] HormingMode m_mode = HormingMode.Strict;

    float m_dir = 0f; //曲がる方向
    
    [SerializeField] float m_playerOffsetY; //プレイヤーよりどれくらい上で動きを変化するか           
    [SerializeField] float m_chasingPower = 1f; //カーブする時にかける力
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        m_rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (m_mode == HormingMode.Strict)
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
        else if (m_mode == HormingMode.Easy)
        {
            if (moving)
            {
                Vector2 force = new Vector2(0, 1);
                m_rb2d.AddForce(force, ForceMode2D.Force);

                if (player.transform.position.y - this.transform.position.y < m_playerOffsetY)
                {
                    // 左右どちらに曲がるか判定する
                    if (m_dir == 0)
                    {
                        m_dir = (player.transform.position.x > this.transform.position.x) ? 1 : -1;

                    }
                    m_rb2d.AddForce(m_dir * Vector2.right * m_chasingPower);
                }
            }
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
public enum HormingMode
{
    Strict,
    Easy,
}
