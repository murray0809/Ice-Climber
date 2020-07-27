using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;    // 移動早さ

    void Update()
    {
        // 矢印キーの入力情報を取得
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // 移動する向きを作成する
        Vector2 direction = new Vector2(h, v).normalized;

        // 移動する向きとスピードを代入 
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
