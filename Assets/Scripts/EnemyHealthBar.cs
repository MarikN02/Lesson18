using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Canvas healthCanvas;
    [SerializeField] private Camera playerCamera;

    [Header("Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);
    [SerializeField] private bool alwaysFaceCamera = true;
    [SerializeField] private Color fullHealthColor = Color.red;
    [SerializeField] private Color lowHealthColor = Color.yellow;

    private void Start()
    {
        // Автопоиск компонентов
        if (enemyHealth == null)
            enemyHealth = GetComponentInParent<Health>();

        if (healthSlider == null)
            healthSlider = GetComponentInChildren<Slider>();

        if (fillImage == null && healthSlider != null)
            fillImage = healthSlider.fillRect.GetComponent<Image>();

        if (healthCanvas == null)
            healthCanvas = GetComponent<Canvas>();

        if (playerCamera == null)
            playerCamera = Camera.main;

        // Настройка World Space Canvas
        if (healthCanvas != null)
        {
            healthCanvas.worldCamera = playerCamera;
            healthCanvas.renderMode = RenderMode.WorldSpace;
        }

        // Начальная настройка
        if (healthSlider != null && enemyHealth != null)
        {
            healthSlider.maxValue = enemyHealth.GetMaxHealth();
            healthSlider.value = enemyHealth.GetCurrentHealth();
        }

        // Скрываем если враг мертв
        if (enemyHealth != null && !enemyHealth.IsAlive())
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enemyHealth == null) return;

        UpdateHealthBar();

        if (alwaysFaceCamera && playerCamera != null)
        {
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                            playerCamera.transform.rotation * Vector3.up);
        }

        // Следим за позицией врага
        if (enemyHealth != null)
        {
            transform.position = enemyHealth.transform.position + offset;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider == null) return;

        healthSlider.value = enemyHealth.GetCurrentHealth();

        if (fillImage != null)
        {
            float healthPercentage = enemyHealth.GetHealthPercentage();
            fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercentage);
        }

        // Скрываем если враг мертв
        if (!enemyHealth.IsAlive())
            gameObject.SetActive(false);
    }

    public void ShowHealthBar()
    {
        gameObject.SetActive(true);
    }

    public void HideHealthBar()
    {
        gameObject.SetActive(false);
    }
}