using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI popupText;

    [Header("Settings")]
    public float moveSpeed = 1.5f;
    public float lifeTime = 1.2f;
    public float fadeTime = 0.8f;

    private Vector3 startPosition;
    private float timer;
    private Color originalColor;

    private void Start()
    {
        // Сохраняем начальную позицию и цвет
        startPosition = transform.position;

        if (popupText != null)
        {
            originalColor = popupText.color;
        }

        // Автоматически находим TextMeshPro если не назначен
        if (popupText == null)
        {
            popupText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Убедимся что Canvas правильно настроен
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
            if (canvas.worldCamera == null)
            {
                canvas.worldCamera = Camera.main;
            }
        }

        // Уничтожаем через время
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(int points)
    {
        if (popupText != null)
        {
            popupText.text = $"+{points}";
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Движение вверх
        transform.position = startPosition + Vector3.up * (moveSpeed * timer);

        // Плавное исчезновение
        if (popupText != null && timer > fadeTime)
        {
            float alpha = 1f - ((timer - fadeTime) / (lifeTime - fadeTime));
            popupText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
    }
}