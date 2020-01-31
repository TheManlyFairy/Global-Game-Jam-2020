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
    [SerializeField] ScriptableTutorial[] tutorialPages;
    [SerializeField] Image tutorialImage;
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI scoreText;

    enum ActiveMenu { Main, Tutorial }
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
        GameManager.instance.onScoreChange += UpdateScore;
    }
    private void Update()
    {
        if (GameManager.CurrentGameMode == GameManager.GameMode.Pause)
        {
            if (activeMenu == ActiveMenu.Main)
            {
                MainMenuInput();
            }
            if (activeMenu == ActiveMenu.Tutorial)
            {
                TutorialMenuInput();
            }
        }
    }

    void MainMenuInput()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.Start))
        {
            mainMenu.SetActive(false);
            GameManager.instance.StartGame();
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft))
        {
            activeMenu = ActiveMenu.Tutorial;
            mainMenu.SetActive(false);
            tutorialMenu.SetActive(true);
        }
    }
    void TutorialMenuInput()
    {
        if (Input.GetKeyDown((KeyCode)DancePadKey.Back))
        {
            activeMenu = ActiveMenu.Main;
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleLeft))
        {
            if (tutorialIndex > 0)
            {
                tutorialIndex--;
                tutorialImage.sprite = tutorialPages[tutorialIndex].sprite;
                tutorialText.text = tutorialPages[tutorialIndex].text;
            }
        }
        if (Input.GetKeyDown((KeyCode)DancePadKey.MiddleRight))
        {
            if (tutorialIndex < tutorialPages.Length-1)
            {
                tutorialIndex++;
                tutorialImage.sprite = tutorialPages[tutorialIndex].sprite;
                tutorialText.text = tutorialPages[tutorialIndex].text;
            }
        }
    }
    void UpdateScore(int score)
    {
        scoreText.text = "Score: "+score;
    }
}
