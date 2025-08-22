using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        // Уничтожаем объект только если у него НЕТ тега "Damageable"
        // (то есть пуля уничтожается, а враги - нет)
        if (!gameObject.CompareTag("Damageable"))
        {
            Destroy(gameObject);
        }
    }
}