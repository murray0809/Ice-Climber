using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circularmotion : MonoBehaviour
{
    public float speed;
    public float radius;
    private Vector2 position;
    float x;
    float y;
    public bool enter = false;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enter)
        {
            x = radius * Mathf.Sin(Time.time * speed);
            y = radius * Mathf.Cos(Time.time * speed);

            transform.position = new Vector2(x + position.x, y + position.y);
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
}
