using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string[] ignoreTags = { "CameraConfiner", "��� ���� ���������� ��� DamageDealer" }; // ���� ��� ������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ����� �� ������������ ���� ���������
        if (ShouldIgnoreCollision(collision))
        {
            return; // ���������� ��������
        }

        if (collision.CompareTag("Damageable"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        // ���������� ������ ������ ���� � ���� ��� ���� "Damageable"
        if (!gameObject.CompareTag("Damageable"))
        {
            Destroy(gameObject);
        }
    }

    private bool ShouldIgnoreCollision(Collider2D collision)
    {
        // ��������� ��� ���� �� ������� ignoreTags
        foreach (string tag in ignoreTags)
        {
            if (collision.CompareTag(tag))
            {
                return true; // ���������� ���� ���������
            }
        }
        return false;
    }
}