using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyctr : MonoBehaviour
{
    public float moveSpeed = 0f;
    Rigidbody2D rbd;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
         player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        dir = dir.normalized;

        // プレイヤーに向かって飛ばす
        rbd.velocity = dir * moveSpeed;
    }
}
