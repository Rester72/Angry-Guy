using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    [Header("Movement")]
    public float maxSpeed = 8f;
    [Tooltip("Lower = more slippery, Higher = more responsive")]
    public float movementResponsiveness = 5f;

    [Header("Rolling")]
    public float rotationMultiplier = 50f;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private float currentSpeed;
    private bool isGrounded;

    void Update()
    {
        if (groundCheck != null)
        {
            groundCheck.rotation = Quaternion.identity;

            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundRadius,
                groundLayer
            );
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        float targetSpeed = input * maxSpeed;

        // Smoothly move toward target speed
        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            movementResponsiveness * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(
            currentSpeed,
            rb.linearVelocity.y
        );

        // Rolling synced with actual movement speed
        rb.angularVelocity = -rb.linearVelocity.x * rotationMultiplier;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundRadius
        );
    }
}