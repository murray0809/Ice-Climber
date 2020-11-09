using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideChange : MonoBehaviour
{
    [SerializeField] private GameObject m_another;
    [SerializeField] private float a = 1.1f;
    private Vector3 transform;
    [SerializeField] private bool isCloud;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform = collision.gameObject.transform.position;
        if (isCloud)
        {
            if (collision.gameObject.layer == 10 && collision.gameObject.GetComponent<CloudController>().isRightMove)
            {
                collision.gameObject.transform.position = new Vector3(m_another.transform.position.x, transform.y, transform.z);
            }
        }
        else
        {
            if (collision.gameObject.layer != 10)
            {
                collision.gameObject.transform.position = new Vector3(m_another.transform.position.x + a, transform.y, transform.z);
            }
        }
        
    }
}
