using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public delegate void ScoreChange(int score);
    public event ScoreChange onScoreChange;

    public int shieldRepairPerPress = 30;
    public enum GameMode { Pause, Play }
    public GameMode gameMode;
    [SerializeField] Transform enemyTarget;
    [SerializeField] Shield[] shields;

    int score;

    public Shield[] ShieldMap { get { return shields; } }
    public Vector3 TargetPosition { get { return enemyTarget.position; } }
    public static GameMode CurrentGameMode { get { return Instance.gameMode; } }

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
        }
    }
    public void StartGame()
    {
        gameMode = GameMode.Play;
        score = 0;
    }
    public void IncrementScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(onScoreChange!=null)
        {
            onScoreChange(score);
        }
    }
}
