using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ---- RUN DATA (shared across levels) ----
    public static int totalScore = 0;
    public static int highScore = 0;

    [Header("UI")]
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text stateText;
    public TMP_Text waveText;

    [Header("Health Bar")]
    public Image healthFill;
    public Gradient healthGradient;

    [Header("Horde / Wave Bar")]
    public Slider hordeBar;
    public Image hordeFill;
    public Gradient hordeGradient;
    public int maxHordeLevel = 3;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject clearPanel;

    [Header("Scene Flow")]
    public string nextSceneName = "";

    [Header("Wave System")]
    public int currentWave = 1;
    public int finalWave = 3;

    [Header("Difficulty")]
    public float zombieSpeedMultiplier = 1f;

    private int score = 0;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (clearPanel != null)
            clearPanel.SetActive(false);

        if (stateText != null)
            stateText.text = "";

        UpdateScoreUI();
        UpdateWaveUI();
    }

    private void Update()
    {
        if (gameEnded) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            RestartLevel();
        }
    }

    // ---------------- RUN DATA ----------------

    public static void ResetRunData()
    {
        totalScore = 0;
    }

    public int GetLevelScore()
    {
        return score;
    }

    // ---------------- SCORE ----------------

    public void AddScore(int amount)
    {
        if (gameEnded) return;

        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // ---------------- HEALTH ----------------

    public void UpdateHealthUI(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = max;
            healthBar.value = current;

            float t = (float)current / max;

            if (healthFill != null)
                healthFill.color = healthGradient.Evaluate(t);
        }
    }

    // ---------------- WAVE ----------------

    public void UpdateWaveUI()
    {
        if (hordeBar != null)
        {
            hordeBar.maxValue = finalWave;
            hordeBar.value = currentWave;

            float t = (float)currentWave / finalWave;

            if (hordeFill != null)
                hordeFill.color = hordeGradient.Evaluate(t);
        }

        if (waveText != null)
        {
            waveText.text = "WAVE " + currentWave;
        }
    }

    // ---------------- GAME STATES ----------------

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (stateText != null)
            stateText.text = "GAME OVER";

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void LevelClear()
    {
        if (gameEnded) return;
        gameEnded = true;

        // add this level's score into total run score
        totalScore += score;

        // update high score if needed
        if (totalScore > highScore)
        {
            highScore = totalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (stateText != null)
            stateText.text = "LEVEL CLEAR";

        if (clearPanel != null)
            clearPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public bool IsGameEnded()
    {
        return gameEnded;
    }

    // ---------------- SCENE CONTROL ----------------

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene Name is empty.");
        }
    }
}