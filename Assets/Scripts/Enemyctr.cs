using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyctr : MonoBehaviour
{
    public float moveSpeed = 0f;
    Rigidbody2D rbd;
    GameObject player;
    bool enter = false;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Debug.Log("侵入");
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dir = player.transform.position - this.transform.position;
            dir = dir.normalized;

            // プレイヤーに向かって飛ばす
            rbd.velocity = dir * moveSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rbd.velocity = dir * 0;
        }
    }

}




