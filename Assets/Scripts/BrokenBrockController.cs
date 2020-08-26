using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrockController : MonoBehaviour
{
    public bool IsUnder;
    //[SerializeField] private bool IsBroken = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsUnder)
        {
            //IsBroken = true;
            Destroy(this.gameObject);
        }
    }
}
