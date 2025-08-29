using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = GetMaxHealth();
            healthSlider.value = GetCurrentHealth();
        }
    }

    private void Update()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = GetCurrentHealth();

            // Меняем цвет в зависимости от уровня здоровья
            if (fillImage != null)
            {
                fillImage.color = GetHealthColor();
            }
        }
    }

    private Color GetHealthColor()
    {
        float healthPercentage = GetCurrentHealth() / GetMaxHealth();

        if (healthPercentage > 0.7f) return Color.green;
        if (healthPercentage > 0.3f) return Color.yellow;
        return Color.red;
    }

    private float GetCurrentHealth()
    {
        // Вам нужно добавить этот метод в класс Health
        return playerHealth.GetCurrentHealth();
    }

    private float GetMaxHealth()
    {
        // Вам нужно добавить этот метод в класс Health
        return playerHealth.GetMaxHealth();
    }
}