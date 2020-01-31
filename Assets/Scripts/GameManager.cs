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
    [SerializeField] Transform enemyTarget;
    [SerializeField] Shield[] shields;
    
    int score;

    public Shield[] ShieldMap
    {
        get { return shields; }
    }

    public Vector3 TargetPosition
    {
        get { return enemyTarget.position; }
    }

    public static GameMode CurrentGameMode
    {
        get { return Instance.gameMode; }
    }

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
        onScoreChange?.Invoke(score);
    }
    public void LoseGame()
    {
        gameMode = GameMode.Pause;
        score = 0;
    }
}

