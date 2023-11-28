using TMPro;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public TextMeshProUGUI gameOverText;
    public int currentScore;
    public int maxScore;
}
