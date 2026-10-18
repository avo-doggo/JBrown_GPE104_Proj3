using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Player")]
    public ControllerPlayer controllerPlayer;
    public PawnShip pawnShip;

    [Header("Game")]
    public List<Obstacle> obstacleList;
    public int score;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text scoreWinText;

    [Header("Screen Boundaries")]
    public float topBorder = 1.5f;
    public float bottomBorder = 1.5f;

    [Header("Player Lives")]
    public int startingLives = 3;
    public int lives;
    public TMP_Text livesText;

    private Camera mainCamera;

    public float screenLeft;
    public float screenRight;
    public float screenTop;
    public float screenBottom;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public bool spawningFinished = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        obstacleList = new List<Obstacle>();

        mainCamera = Camera.main;

        CalculateScreenBounds();
    }

    private void Start()
    {
        lives = startingLives;
        UpdateLivesUI();
    }

    public void LoseLife()
    {
        lives--;

        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        
        }
       /* else
        {
            RespawnPlayer();
        }*/
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = lives.ToString();
        }
    }

    public void GameOver()
    {
        Destroy(controllerPlayer.gameObject);
       // if (gameOverPanel != null)
        //{
           // gameOverPanel.SetActive(true);
            
        //}
    }

    public void CheckForVictory()
    {
        // Don't check for victory until the spawner is finished.
        if (!spawningFinished)
        {
            return;
        }

        // If enemies are still alive, don't win.
        if (obstacleList.Count > 0)
        {
            return;
        }

        // Show victory screen.
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (scoreWinText != null)
        {
            scoreWinText.text = score.ToString();
        }

        Debug.Log("You Win!");
    }

   /* public void RespawnPlayer()
    {
        if (controllerPlayer != null)
        {
            controllerPlayer.Respawn();
        }
    }*/

    private void CalculateScreenBounds()
    {
        if (mainCamera == null)
        {
            return;
        }

        // Get the bottom-left corner of the camera
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0, 0, -mainCamera.transform.position.z)
        );

        // Get the top-right corner of the camera
        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1, 1, -mainCamera.transform.position.z)
        );

        // Left and right edges of the screen
        screenLeft = bottomLeft.x;
        screenRight = topRight.x;

        // Top and bottom edges, leaving room for the UI bars
        screenBottom = bottomLeft.y + bottomBorder;
        screenTop = topRight.y - topBorder;
    }


    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
    }


    public void AddObstacle(Obstacle obstacle)
    {
        if (obstacle != null && !obstacleList.Contains(obstacle))
        {
            obstacleList.Add(obstacle);
        }
    }


    public void RemoveObstacle(Obstacle obstacle)
    {
        if (obstacle != null && obstacleList.Contains(obstacle))
        {
            obstacleList.Remove(obstacle);

            // Check for victory after an enemy has been removed.
            CheckForVictory();
        }
    }


}