using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] Image gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    [SerializeField] TextMeshProUGUI scoreText;

    int score;

    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        HideGameOver();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void ShowGameOver()
    {
        scoreText.gameObject.SetActive(false);
        gameOverScoreText.text = score.ToString();
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        Time.timeScale = 1f;
        scoreText.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
