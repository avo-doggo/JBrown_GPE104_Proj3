using UnityEngine;

public class DeathDestroyManager : Death
{
    public int scoreValue;

    private Obstacle obstacleToRemoveOnDeath;

    public override void Die()
    {
        // Check if this object is an asteroid.
        Asteroids asteroid = GetComponent<Asteroids>();

        if (asteroid != null)
        {
            // Create the smaller asteroids before destroying this one.
            asteroid.SplitAsteroid();
        }

        // Remove the asteroid/enemy from the obstacle list.
        if (LevelManager.instance != null &&
            obstacleToRemoveOnDeath != null)
        {
            LevelManager.instance.RemoveObstacle(obstacleToRemoveOnDeath);

            LevelManager.instance.AddScore(scoreValue);
        }

        // Play the destruction sound.
        if (GameManager.instance != null &&
            GameManager.instance.destructionSound != null)
        {
            AudioSource.PlayClipAtPoint(
                GameManager.instance.destructionSound,
                transform.position,
                GameManager.instance.SFXVolume
            );
        }

        // Destroy the current asteroid.
        Destroy(gameObject);
    }

    public override void Start()
    {
        obstacleToRemoveOnDeath = GetComponent<Obstacle>();
    }

    public override void Update()
    {

    }
}