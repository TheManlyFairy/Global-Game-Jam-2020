using UnityEngine;
using Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public delegate void ScoreChange(int score);
    
    public event ScoreChange OnScoreChange;
    public event System.Action OnGameOver;
    
    public int shieldRepairPerPress = 30;
    public GameMode gameMode;

    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public Vector3 TargetPosition => enemyTarget.position;
    public Shield[] ShieldMap => shields;
    public static GameMode CurrentGameMode => Instance.gameMode;

    [SerializeField] private Transform enemyTarget;
    [SerializeField] private Shield[] shields;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
            gameMode = GameMode.Pause;
            Score = 0;
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    public void StartGame()
    {
        gameMode = GameMode.Play;
        Score = 0;

        ResetShields();
    }

    public void IncrementScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        OnScoreChange?.Invoke(Score);
    }

    public void GameOver()
    {
        gameMode = GameMode.Pause;
        HighScore = Score > HighScore ? Score : HighScore;
        OnGameOver?.Invoke();
    }

    private void ResetShields()
    {
        foreach (Shield shield in shields)
        {
            shield.ResetShield();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
    }
}

