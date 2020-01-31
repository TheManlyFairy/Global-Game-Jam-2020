using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject tutorialMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] ScriptableTutorial[] tutorialPages;
    [SerializeField] Image tutorialImage;
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI playTime;
    [SerializeField] Spawner spawnManager;

    ActiveMenu activeMenu;
    int tutorialIndex = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
            activeMenu = ActiveMenu.Main;
            tutorialImage.sprite = tutorialPages[0].sprite;
            tutorialText.text = tutorialPages[0].text;
            scoreText.text = "Score: 0";
        }
    }
    private void Start()
    {
        GameManager.Instance.onScoreChange += UpdateScore;
        Shield.onShieldBreak += PopupLoseMenu;
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

    public void PopupLoseMenu()
    {
        activeMenu = ActiveMenu.Lose;
        scoreText.gameObject.SetActive(false);
        loseMenu.SetActive(true);
        scoreText.text = "Score: 0";
        playerScore.text = "Your Score: " + GameManager.Instance.Score;
        highScore.text = "High Score: " + GameManager.Instance.HighScore;
    }
    void MainMenuInput()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.Start) || Input.GetKeyDown(KeyCode.S))
        {
            mainMenu.SetActive(false);
            GameManager.Instance.StartGame();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            activeMenu = ActiveMenu.Tutorial;
            mainMenu.SetActive(false);
            tutorialMenu.SetActive(true);
        }
    }
    void TutorialMenuInput()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.Back) || Input.GetKeyDown(KeyCode.Backspace))
        {
            activeMenu = ActiveMenu.Main;
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft) || Input.GetKeyDown(KeyCode.A))
        {
            if (tutorialIndex > 0)
            {
                tutorialIndex--;
                tutorialImage.sprite = tutorialPages[tutorialIndex].sprite;
                tutorialText.text = tutorialPages[tutorialIndex].text;
            }
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight) || Input.GetKeyDown(KeyCode.D))
        {
            if (tutorialIndex < tutorialPages.Length - 1)
            {
                tutorialIndex++;
                tutorialImage.sprite = tutorialPages[tutorialIndex].sprite;
                tutorialText.text = tutorialPages[tutorialIndex].text;
            }
        }
    }
    void LoseMenuInput()
    {
        if(Input.GetKeyDown((KeyCode)DancePadKey.Start) || Input.GetKeyDown(KeyCode.S))
        {
            loseMenu.SetActive(false);
            spawnManager.Restart();
            GameManager.Instance.StartGame();
            scoreText.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.Back) || Input.GetKeyDown(KeyCode.Backspace))
        {
            activeMenu = ActiveMenu.Main;
            loseMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
