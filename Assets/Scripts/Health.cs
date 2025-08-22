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

    // Публичный метод для проверки состояния
    public bool IsAlive()
    {
        return isAlive;
    }
}


//Теперь другие скрипты могут проверять состояние объекта через:
//Health health = GetComponent<Health>();
//if (health.IsAlive())
//{
    // Объект жив
//}
//else
//{
    // Объект мертв
//}