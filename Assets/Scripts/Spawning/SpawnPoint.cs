using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum SpawnType
    {
        HealthPack,
        Astronaut,
        EnemyShip,
        Asteroid
    }

    [Header("Spawn Settings")]
    public SpawnType spawnType;
}