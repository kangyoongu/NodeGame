using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playingscoreText;
    public TextMeshProUGUI bestText;
    private int currentScore;
    public int CurrentScore {
        get => currentScore;
        set
        {
            currentScore = value;
            if (currentScore > PlayerPrefs.GetInt("Best"))
            {
                PlayerPrefs.SetInt("Best", currentScore);
                bestText.text = "Best : " + PlayerPrefs.GetInt("Best").ToString("0");
            }
            scoreText.text = "Score : " + currentScore.ToString("0");
            playingscoreText.text = "Score " + currentScore.ToString("0");
        }
    }
    public void Start()
    {
        if (!PlayerPrefs.HasKey("Best"))
        {
            PlayerPrefs.SetInt("Best", 0);
        }
        bestText.text = PlayerPrefs.GetInt("Best").ToString("0");
    }
    public void GameOver()
    {
        UIManager.Instance.GameOverUIIn();
        UIManager.Instance.PlayUIOut();
        GameManager.Instance.playingGame = false;
    }
    public void OnClickGoToMain()
    {
        SceneManager.LoadScene(0);
    }
}
