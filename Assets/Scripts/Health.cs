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

    // ДОБАВЛЕННЫЕ МЕТОДЫ ДЛЯ ОТОБРАЖЕНИЯ ЗДОРОВЬЯ:

    /// <summary>
    /// Возвращает текущее количество здоровья
    /// </summary>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Возвращает максимальное количество здоровья
    /// </summary>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Возвращает процент здоровья от 0 до 1
    /// </summary>
    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}