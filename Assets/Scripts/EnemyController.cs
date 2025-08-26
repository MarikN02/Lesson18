using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float TimeToRevert; // Добавлен тип float
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sp;

    private Rigidbody2D rb;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;

    private float currentState;
    private float currentTimeToRevert; // Добавлен тип float

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
            currentTimeToRevert = 0; // Исправлено : на ;
            currentState = REVERT_STATE;
        }

        switch (currentState)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;
            case WALK_STATE:
                rb.velocity = Vector2.right * Speed; // Исправлено left на right
                break;
            case REVERT_STATE:
                sp.flipX = !sp.flipX;
                Speed *= -1;
                currentState = WALK_STATE; // Добавлено возвращение в состояние ходьбы
                break;
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Еще одно исключение для DamageDealer")) // Укажите правильный тег
        {
            currentState = IDLE_STATE;
        }
    }
}