using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [Header("Asteroid Movement")]
    public float force;
    public float lifetime;

    [Header("Asteroid Splitting")]
    public GameObject smallerAsteroidPrefab;
    public int numberOfSmallerAsteroids;

    private Rigidbody rb;
    private Transform tf;

    private Obstacle obstacleToRemoveOnDeath;

    void Start()
    {
        // Get the Obstacle component.
        obstacleToRemoveOnDeath = GetComponent<Obstacle>();

        // Get the Rigidbody2D.
        rb = GetComponent<Rigidbody>();

        // Get the Transform.
        tf = GetComponent<Transform>();

        // Give the asteroid a random direction.
        if (rb != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            rb.AddForce(randomDirection * force);
        }

        // Destroy the asteroid after its lifetime.
        Destroy(gameObject, lifetime);
    }

    // Called when this asteroid is destroyed by its Death script.
    public void SplitAsteroid()
    {
        // Make sure a smaller asteroid prefab has been assigned.
        if (smallerAsteroidPrefab == null)
        {
            return;
        }

        // Create the number of smaller asteroids specified in the Inspector.
        for (int i = 0; i < numberOfSmallerAsteroids; i++)
        {
            // Give each asteroid a slightly random direction.
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;

            // Spawn the smaller asteroid.
            GameObject smallerAsteroid = Instantiate(
                smallerAsteroidPrefab,
                transform.position + (Vector3)randomOffset,
                Quaternion.identity
            );
        }
    }

    private void OnDestroy()
    {
        // Remove this asteroid from the obstacle list.
        if (LevelManager.instance != null &&
            obstacleToRemoveOnDeath != null)
        {
            LevelManager.instance.RemoveObstacle(obstacleToRemoveOnDeath);
        }
    }

    void Update()
    {
    }
}