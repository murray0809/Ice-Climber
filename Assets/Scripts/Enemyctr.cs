using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyctr : MonoBehaviour
{
    public float moveSpeed = 0f;
    Rigidbody2D rbd;
    GameObject player;
    Vector2 dir;
    RoundTripEnemy roundTripEnemy;
    circularmotion circularmotion;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        roundTripEnemy = GetComponent<RoundTripEnemy>();
        circularmotion = GetComponent<circularmotion>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (roundTripEnemy)
            {
                roundTripEnemy.enter = true;
            }
            if (circularmotion)
            {
                circularmotion.enter = true;
            }
            dir = player.transform.position - this.transform.position;
            dir = dir.normalized;

            // プレイヤーに向かって飛ばす
            rbd.velocity = dir * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("痛い");
        if (collision.gameObject.tag == "Attack")
        {
            
            Destroy(this.gameObject);
        }  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (roundTripEnemy)
        {
            roundTripEnemy.enter = false;
        }
        if (circularmotion)
        {
            circularmotion.enter = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            rbd.velocity = dir * 0;
        }
    }

}




