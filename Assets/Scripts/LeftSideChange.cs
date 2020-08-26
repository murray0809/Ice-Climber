using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideChange : MonoBehaviour
{
    [SerializeField] private GameObject m_another;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 transform = collision.gameObject.transform.position;
        collision.gameObject.transform.position = new Vector3(m_another.transform.position.x - 1.1f, transform.y, transform.z);
    }
}
