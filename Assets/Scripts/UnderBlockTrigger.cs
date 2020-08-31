using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderBlockTrigger : MonoBehaviour
{
    [SerializeField] public GameObject m_block;
    private BrokenBrockController BrokenBrockController;

    void Start()
    {
        BrokenBrockController = transform.parent.GetComponent<BrokenBrockController>();
    }

    //private void Update()
    //{
    //    BrokenBrockController.IsUnder = false;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BrokenBrockController.IsUnder = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BrokenBrockController.IsUnder = false;
        }
        
    }
}
