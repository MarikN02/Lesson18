using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string[] ignoreTags = { "CameraConfiner", "Еще одно исключение для DamageDealer" }; // Теги для игнора

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, нужно ли игнорировать этот коллайдер
        if (ShouldIgnoreCollision(collision))
        {
            return; // Игнорируем коллизию
        }

        if (collision.CompareTag("Damageable"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        // Уничтожаем объект только если у него НЕТ тега "Damageable"
        if (!gameObject.CompareTag("Damageable"))
        {
            Destroy(gameObject);
        }
    }

    private bool ShouldIgnoreCollision(Collider2D collision)
    {
        // Проверяем все теги из массива ignoreTags
        foreach (string tag in ignoreTags)
        {
            if (collision.CompareTag(tag))
            {
                return true; // Игнорируем этот коллайдер
            }
        }
        return false;
    }
}