using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private bool isGrounded = false;

    [Header("Settings")]
    [SerializeField] private Transform groundCollaiderTransform;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float jumpOffset;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private PlayerAnimator playerAnimator;

    // Public property дл€ доступа к isGrounded из других скриптов
    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundCollaiderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
    }

    public void Move(float direction, bool isJumpButtonPressed)
    {
        if (direction > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction < 0 && isFacingRight)
        {
            Flip();
        }

        if (isJumpButtonPressed)
            Jump();

        if (Mathf.Abs(direction) > 0.01f)
            HorizontalMovement(direction);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // ¬ключаем анимацию прыжка при нажатии кнопки
            playerAnimator.TriggerJumpAnimation();
        }
    }

    private void HorizontalMovement(float direction)
    {
        rb.velocity = new Vector2(curve.Evaluate(direction) * speed, rb.velocity.y);
    }


}