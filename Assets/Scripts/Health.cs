using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (currentHealth > 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    // ��������� ����� ��� �������� ���������
    public bool IsAlive()
    {
        return isAlive;
    }
}


//������ ������ ������� ����� ��������� ��������� ������� �����:
//Health health = GetComponent<Health>();
//if (health.IsAlive())
//{
    // ������ ���
//}
//else
//{
    // ������ �����
//}