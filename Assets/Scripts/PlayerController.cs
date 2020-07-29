using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;    // 移動早さ

    float jumpForce = 500.0f;       // ジャンプ時に加える力

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 水平方向の入力を検出する
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            // 入力に応じてパドルを水平方向に動かす
            rb.velocity = h * Vector2.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rb.AddForce(transform.up * this.jumpForce);
        }
    }
}
