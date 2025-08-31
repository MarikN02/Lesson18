using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float TimeToRevert;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;

    private Rigidbody2D rb;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;

    private float currentState;
    private float currentTimeToRevert;
    private bool isFacingRight = false;

    private void Start()
    {
        currentState = WALK_STATE;
        currentTimeToRevert = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentTimeToRevert >= TimeToRevert)
        {
            currentTimeToRevert = 0;
            currentState = REVERT_STATE;
        }

        switch (currentState)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                rb.velocity = Vector2.zero;
                break;
            case WALK_STATE:
                rb.velocity = Vector2.right * Speed;
                break;
            case REVERT_STATE:
                Flip();
                Speed *= -1;
                currentState = WALK_STATE;
                break;
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isFacingRight ? 1f : -1f);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Еще одно исключение для DamageDealer"))
        {
            currentState = IDLE_STATE;
        }

        if (collision.CompareTag("Player"))
        {
            anim.SetBool("IsAttacking", true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}