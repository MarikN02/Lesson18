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
        // ������� ����������� �����
        CreateScorePopup();

        // ����������� ����
        PlaySound();

        // ��������� ����
        UpdateScore();

        // ���������� ������
        Destroy(gameObject);
    }

    private void CreateScorePopup()
    {
        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);

            // ����������� popup
            ScorePopup popupScript = popup.GetComponent<ScorePopup>();
            if (popupScript != null)
            {
                popupScript.Initialize(scoreValue);
            }

            if (showDebugMessages) Debug.Log("����������� ����� ������");
        }
        else
        {
            Debug.LogWarning("ScorePopupPrefab �� �������� �� ������!");
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
        // ���� ��� ������� � �����
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
        {
            // ���� ��������� ������� ����� �� �������� � Screen Space
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

        Debug.LogWarning("��������� ������� ����� �� ������!");
    }

    private void UpdateScoreText(TextMeshProUGUI scoreText)
    {
        string currentText = scoreText.text;

        // ������ ������� ����
        if (currentText.Contains(":"))
        {
            string scoreNumber = currentText.Split(':')[1].Trim();
            if (int.TryParse(scoreNumber, out int currentScore))
            {
                currentScore += scoreValue;
                scoreText.text = $"����: {currentScore}";
                if (showDebugMessages) Debug.Log($"���� ���������: {currentScore}");
                return;
            }
        }

        // ���� �� ���������� ����������, ������� ����� �����
        scoreText.text = $"����: {scoreValue}";
    }
}