using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Debug.Log(rb.velocity);
        //rb.velocity = new Vector2(speed, 0);
        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
