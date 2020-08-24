using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderBlockTrigger : MonoBehaviour
{
    [SerializeField] public GameObject m_block;
    private BrokenBrockController BrokenBrockController;

    void Start()
    {
        BrokenBrockController = m_block.GetComponent<BrokenBrockController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BrokenBrockController.IsUnder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BrokenBrockController.IsUnder = false;
    }
}
