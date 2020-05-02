using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [Range(0, 1f)] [SerializeField] float damping;
    Rigidbody2D rb;

    public float velocity { get; set; }
    private void Awake()
    {
        #region Getting
        rb = GetComponent<Rigidbody2D>();
        #endregion
    }

    private void FixedUpdate()
    {
        Vector2 rbVelocity = rb.velocity;
        rbVelocity += velocity * speed * Vector2.right;
        rbVelocity.x *= Mathf.Pow(1f - damping, Time.deltaTime * 10f);
        rb.velocity = rbVelocity;
    }

    public void Jump()
    {
        Vector2 rbVelocity = rb.velocity;
        rbVelocity.y = jumpForce;
        rb.velocity = rbVelocity;
    }
}
