using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrockController : MonoBehaviour
{
    public bool IsUnder = false;
    //[SerializeField] private bool IsBroken = false;

    //private void Update()
    //{
    //    IsUnder = false;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsUnder && collision.gameObject.tag == "Player")
        {
            //IsBroken = true;
            Destroy(this.gameObject);
        }
    }
}
