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
        rb.velocity = new Vector2(speed, 0);
    }
}
