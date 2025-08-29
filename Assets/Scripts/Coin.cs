using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    public int scoreValue = 10;
    public GameObject scorePopupPrefab;
    public AudioClip collectSound;

    [Header("Debug")]
    public bool showDebugMessages = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        // Создаем всплывающий текст
        CreateScorePopup();

        // Проигрываем звук
        PlaySound();

        // Обновляем счет
        UpdateScore();

        // Уничтожаем монету
        Destroy(gameObject);
    }

    private void CreateScorePopup()
    {
        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);

            // Настраиваем popup
            ScorePopup popupScript = popup.GetComponent<ScorePopup>();
            if (popupScript != null)
            {
                popupScript.Initialize(scoreValue);
            }

            if (showDebugMessages) Debug.Log("Всплывающий текст создан");
        }
        else
        {
            Debug.LogWarning("ScorePopupPrefab не назначен на монете!");
        }
    }

    private void PlaySound()
    {
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
    }

    private void UpdateScore()
    {
        // Ищем все канвасы в сцене
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
        {
            // Ищем текстовый элемент счета на канвасах в Screen Space
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay ||
                canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                TextMeshProUGUI scoreText = canvas.GetComponentInChildren<TextMeshProUGUI>();
                if (scoreText != null && scoreText.gameObject.name.Contains("Score"))
                {
                    UpdateScoreText(scoreText);
                    return;
                }
            }
        }

        Debug.LogWarning("Текстовый элемент счета не найден!");
    }

    private void UpdateScoreText(TextMeshProUGUI scoreText)
    {
        string currentText = scoreText.text;

        // Парсим текущие очки
        if (currentText.Contains(":"))
        {
            string scoreNumber = currentText.Split(':')[1].Trim();
            if (int.TryParse(scoreNumber, out int currentScore))
            {
                currentScore += scoreValue;
                scoreText.text = $"Очки: {currentScore}";
                if (showDebugMessages) Debug.Log($"Очки обновлены: {currentScore}");
                return;
            }
        }

        // Если не получилось распарсить, создаем новый текст
        scoreText.text = $"Очки: {scoreValue}";
    }
}