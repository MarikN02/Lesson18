using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public Transform player;
    public Animator animator;
    public Collider2D attackTrigger;

    private Rigidbody2D rb;
    private bool shouldChase = false;
    private bool canAttack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        shouldChase = distance <= detectionRange;

        if (shouldChase)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }

        animator.SetBool("IsAttacking", canAttack);
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // ИСПРАВЛЕННЫЙ ПОВОРОТ:
        if (direction.x > 0.1f)
        {
            // Игрок справа - поворачиваем вправо (обычный масштаб)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < -0.1f)
        {
            // Игрок слева - поворачиваем влево (отражаем по X)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void StopChasing()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetFloat("Speed", 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canAttack = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}