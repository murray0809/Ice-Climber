using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideChange : MonoBehaviour
{
    [SerializeField] private GameObject m_another;
    [SerializeField] private float a = 1.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            a = 3.0f;
        }
        else
        {
            a = 1.1f;
        }
        Vector3 transform = collision.gameObject.transform.position;
        collision.gameObject.transform.position = new Vector3(m_another.transform.position.x - a, transform.y, transform.z);
    }
}
