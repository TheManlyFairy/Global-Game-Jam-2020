using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public delegate void ScoreChange(int score);
    public event ScoreChange onScoreChange;
    public int shieldRepairPerPress = 30;
    public GameMode gameMode;

    public int Score
    {
        get
        {
            return score;
        }
    }
    public int HighScore
    {
        get
        {
            return highScore;
        }
    }
    public Vector3 TargetPosition
    {
        get { return enemyTarget.position; }
    }
    public Shield[] ShieldMap
    {
        get { return shields; }
    }
    public static GameMode CurrentGameMode
    {
        get { return Instance.gameMode; }
    }

    [SerializeField] Transform enemyTarget;
    [SerializeField] Shield[] shields;

    int score;
    int highScore;

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
            Shield.onShieldBreak += LoseGame;
            score = 0;
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    public void StartGame()
    {
        gameMode = GameMode.Play;
        score = 0;

        ResetShields();
    }
    public void IncrementScore(int scoreToAdd)
    {
        score += scoreToAdd;
        onScoreChange?.Invoke(score);
    }
    public void LoseGame()
    {
        gameMode = GameMode.Pause;
        highScore = score > highScore ? score : highScore;
    }
    void ResetShields()
    {
        for (int i = 0; i < shields.Length; i++)
        {
            shields[i].ResetShield();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}

