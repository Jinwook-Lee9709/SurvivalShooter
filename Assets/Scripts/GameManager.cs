using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseUICanvas;
    [SerializeField] private Canvas gameOverUICanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    public bool isPaused = false;
    public bool isEnded = false;
    private PauseUI pauseUI;
    private PlayerHealth health;

    private float score;
    private void Start()
    {
        pauseUI = pauseUICanvas.GetComponent<PauseUI>();
        pauseUI.OnResume += ResumeGame;
        health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        health.onDeath += OnPlayerDie;
    }

    private void Update()
    {
        if (!isEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseUI.gameObject.SetActive(true);
            isPaused = true;
        }

        if (isEnded && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnPlayerDie()
    {
        isPaused = true;
        isEnded = true;
        gameOverUICanvas.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
        isPaused = false;
    }

    public void OnGetScore(int score)
    {
        this.score += score;
        scoreText.text = $"SCORE: {this.score}";
    }
    
    
}
