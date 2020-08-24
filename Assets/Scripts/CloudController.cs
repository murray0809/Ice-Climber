using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private float x;

    void Update()
    {
        x += speed;
        transform.position = new Vector2(x, 0);
    }
}
