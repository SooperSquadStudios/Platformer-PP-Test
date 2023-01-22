using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [HideInInspector] public bool isGameOver;

    public GameObject gameOverPanel;

    public float score;

    [SerializeField] private TMP_Text scoreText;

    public float highScore;
    public TMP_Text highScoreText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        gameOverPanel.SetActive(false);
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "HighScore: " + highScore.ToString("0");
    }
    private void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 5;
            scoreText.text = "Score: " + score.ToString("0");
        }
    }
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
