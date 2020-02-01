using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private ScriptableTutorial[] tutorialPages;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private Spawner spawnManager;

    private ActiveMenu activeMenu;
    private int _tutorialIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
            activeMenu = ActiveMenu.Main;
            tutorialImage.sprite = tutorialPages[0].sprite;
            tutorialText.text = tutorialPages[0].text;
            scoreText.text = "Score: 0";
        }
    }

    private void Start()
    {
        GameManager.Instance.OnScoreChange += UpdateScore;
        GameManager.Instance.OnGameOver += PopupLoseMenu;
    }

    private void Update()
    {
        if (GameManager.CurrentGameMode == GameMode.Pause)
        {
            switch (activeMenu)
            {
                case ActiveMenu.Main:
                    MainMenuInput();
                    break;
                case ActiveMenu.Tutorial:
                    TutorialMenuInput();
                    break;
                case ActiveMenu.Lose:
                    LoseMenuInput();
                    break;
            }
        }
    }

    private void PopupLoseMenu()
    {
        activeMenu = ActiveMenu.Lose;
        scoreObject.SetActive(false);
        loseMenu.SetActive(true);
        scoreText.text = "Score: 0";
        playerScore.text = "Your Score: " + GameManager.Instance.Score;
        highScore.text = "High Score: " + GameManager.Instance.HighScore;
    }

    private void MainMenuInput()
    {
        if (Input.GetKeyDown((KeyCode) DancePadKey.Start) || Input.GetKeyDown(KeyCode.S))
        {
            mainMenu.SetActive(false);
            GameManager.Instance.StartGame();
            scoreObject.SetActive(true);
        }

        if (Input.GetKeyDown((KeyCode) DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            activeMenu = ActiveMenu.Tutorial;
            mainMenu.SetActive(false);
            tutorialMenu.SetActive(true);
        }
    }

    private void TutorialMenuInput()
    {
        if (Input.GetKeyDown((KeyCode) DancePadKey.Back) || Input.GetKeyDown(KeyCode.Backspace))
        {
            activeMenu = ActiveMenu.Main;
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (Input.GetKeyDown((KeyCode) DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            if (_tutorialIndex > 0)
            {
                _tutorialIndex--;
                tutorialImage.sprite = tutorialPages[_tutorialIndex].sprite;
                tutorialText.text = tutorialPages[_tutorialIndex].text;
            }
        }

        if (Input.GetKeyDown((KeyCode) DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            if (_tutorialIndex < tutorialPages.Length - 1)
            {
                _tutorialIndex++;
                tutorialImage.sprite = tutorialPages[_tutorialIndex].sprite;
                tutorialText.text = tutorialPages[_tutorialIndex].text;
            }
        }
    }

    private void LoseMenuInput()
    {
        if (Input.GetKeyDown((KeyCode) DancePadKey.Start) || Input.GetKeyDown(KeyCode.S))
        {
            loseMenu.SetActive(false);
            spawnManager.Restart();
            GameManager.Instance.StartGame();
            scoreObject.SetActive(true);
        }

        if (Input.GetKeyDown((KeyCode) DancePadKey.Back) || Input.GetKeyDown(KeyCode.Backspace))
        {
            activeMenu = ActiveMenu.Main;
            loseMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
