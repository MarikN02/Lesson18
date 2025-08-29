using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Settings")]
    public TMP_Text scoreText;
    public GameObject scorePopupPrefab;

    private int totalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // �������������� ����� ���� �� �� ��������
        if (scoreText == null)
        {
            FindScoreText();
        }
    }

    private void FindScoreText()
    {
        // ��������� ����� ��������� ������� �������������
        GameObject scoreObj = GameObject.Find("ScoreText");
        if (scoreObj != null)
        {
            scoreText = scoreObj.GetComponent<TMP_Text>();
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText �� ������! �������� TMP_Text ������ � ������ 'ScoreText'");
        }
    }

    public void AddScore(int points, Vector2 position)
    {
        totalScore += points;
        UpdateScoreUI();

        // ���������� ����������� �������
        if (scorePopupPrefab != null)
        {
            ShowScorePopup(points, position);
        }
        else
        {
            Debug.LogWarning("ScorePopupPrefab �� ��������!");
        }
    }

    private void ShowScorePopup(int points, Vector2 position)
    {
        GameObject popup = Instantiate(scorePopupPrefab, position, Quaternion.identity);
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();

        if (popupScript != null)
        {
            popupScript.Initialize(points);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"����: {totalScore}";
        }
    }
}