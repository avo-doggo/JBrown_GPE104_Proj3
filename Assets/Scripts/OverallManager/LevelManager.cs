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

    [Header("Player Boundaries")]
    public float maxYPosition;

    [Header("Player Lives")]
    public int startingLives = 3;
    public int lives;
    public TMP_Text livesText;

    [Header("Spawn Prefabs")]
    public GameObject healthPackPrefab;
    public GameObject astronautPrefab;
    public GameObject enemyShipPrefab;
    public GameObject asteroidPrefab;

    [Header("Spawn Points")]
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [Header("Entity Counts")]
    public int healthPackCount;
    public int astronautCount;
    public int enemyShipCount;
    public int asteroidCount;

    private Camera mainCamera;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    private bool spawningFinished = false;


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

        // Automatically find all spawn points in this level.
        SpawnPoint[] foundSpawnPoints =
            FindObjectsByType<SpawnPoint>();

        spawnPoints.AddRange(foundSpawnPoints);

        SpawnEntities();
    }

    //START
    private void Start()
    {
        lives = startingLives;
        UpdateLivesUI();
    }


    //UPDATES

    private void FixedUpdate()
    {
        CheckPlayerHeight();
    }

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }




    //Max height
    public void CheckPlayerHeight()
    {
        if (pawnShip == null)
            return;

        Vector3 playerPosition = pawnShip.transform.position;

        if (playerPosition.y > maxYPosition)
        {
            playerPosition.y = maxYPosition;

            pawnShip.transform.position = playerPosition;

            Rigidbody rb = pawnShip.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 velocity = rb.linearVelocity;

                if (velocity.y > 0)
                {
                    velocity.y = 0;
                    rb.linearVelocity = velocity;
                }
            }
        }
    }


    //For spawning ent6ities
    private void SpawnEntities()
    {
        SpawnHealthPacks();
        SpawnAstronauts();
        SpawnEnemyShips();
        SpawnAsteroids();
    }
    //for spawning healthpacks
    private void SpawnHealthPacks()
    {
        if (healthPackPrefab == null)
        {
            Debug.LogWarning("Health Pack Prefab has not been assigned!");
            return;
        }

        List<SpawnPoint> availablePoints = new List<SpawnPoint>();

        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.spawnType == SpawnPoint.SpawnType.HealthPack)
            {
                availablePoints.Add(point);
            }
        }
        //spawn random points
        ShuffleList(availablePoints);

        int amountToSpawn = Mathf.Min(
            healthPackCount,
            availablePoints.Count
        );
        //how many random point spawn
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(
                healthPackPrefab,
                availablePoints[i].transform.position,
                availablePoints[i].transform.rotation
            );
        }
    }

    private void SpawnAstronauts()
    {
        if (astronautPrefab == null)
        {
            Debug.LogWarning("Astronaut Prefab has not been assigned!");
            return;
        }

        List<SpawnPoint> availablePoints = new List<SpawnPoint>();

        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.spawnType == SpawnPoint.SpawnType.Astronaut)
            {
                availablePoints.Add(point);
            }
        }

        ShuffleList(availablePoints);

        int amountToSpawn = Mathf.Min(
            astronautCount,
            availablePoints.Count
        );

        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(
                astronautPrefab,
                availablePoints[i].transform.position,
                availablePoints[i].transform.rotation
            );
        }
    }


    private void SpawnEnemyShips()
    {
        if (enemyShipPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab has not been assigned!");
            return;
        }

        List<SpawnPoint> availablePoints = new List<SpawnPoint>();

        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.spawnType == SpawnPoint.SpawnType.EnemyShip)
            {
                availablePoints.Add(point);
            }
        }
        //spawn random points
        ShuffleList(availablePoints);

        int amountToSpawn = Mathf.Min(
            enemyShipCount,
            availablePoints.Count
        );
        //how many random point spawn
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(
                enemyShipPrefab,
                availablePoints[i].transform.position,
                availablePoints[i].transform.rotation
            );
        }
    }

    private void SpawnAsteroids()
    {
        if (asteroidPrefab == null)
        {
            Debug.LogWarning("Asteroid Prefab has not been assigned!");
            return;
        }

        List<SpawnPoint> availablePoints = new List<SpawnPoint>();

        foreach (SpawnPoint point in spawnPoints)
        {
            if (point.spawnType == SpawnPoint.SpawnType.Asteroid)
            {
                availablePoints.Add(point);
            }
        }
        //spawn random points
        ShuffleList(availablePoints);

        int amountToSpawn = Mathf.Min(
            asteroidCount,
            availablePoints.Count
        );
        //how many random point spawn
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(
                asteroidPrefab,
                availablePoints[i].transform.position,
                availablePoints[i].transform.rotation
            );
        }
    }

    private void ShuffleList(List<SpawnPoint> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);

            SpawnPoint temp = list[i];

            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
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
    if (!spawningFinished)
    {
        return;
    }

    if (obstacleList.Count > 0)
    {
        return;
    }

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