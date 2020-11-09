using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrockController : MonoBehaviour
{
    public bool IsUnder = false;
    public GameObject particle;
    new ParticleSystem  particleSystem;
    //[SerializeField] private bool IsBroken = false;

    private void Start()
    {
        particleSystem = particle.GetComponent<ParticleSystem>();
    }

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

    private void OnDestroy()
    {
        Instantiate(particle, this.gameObject.transform.position, Quaternion.identity);
    }
}
