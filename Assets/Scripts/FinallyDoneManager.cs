using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinallyDoneManager : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text highScoreText;

    private void Start()
    {
        if (totalScoreText != null)
            totalScoreText.text = "TOTAL SCORE: " + GameManager.totalScore;

        if (highScoreText != null)
            highScoreText.text = "HIGH SCORE: " + GameManager.highScore;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        GameManager.ResetRunData();
        SceneManager.LoadScene("Level1_Outpost");
    }
}