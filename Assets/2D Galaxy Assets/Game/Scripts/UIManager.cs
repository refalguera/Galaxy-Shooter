using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public int score = 0;
    public Text playerScore;
    public GameObject menu;

    public void Start()
    {
            score = 0;
            playerScore.text = "Score:" + score;
            
    }

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        playerScore.text = "Score:" + score;
    }

    public void ShowTitleScreen()
    {
        menu.SetActive(true);

    }

    public void HideTitleScreen()
    {
        menu.SetActive(false);
        playerScore.text = "Score:";
    }
}
