using UnityEngine;

public class BulletSpawner : Spawner
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public override void Shoot()
    {
        // Create the bullet
        if (bulletSpawnPoint != null && bulletPrefab != null)
        {
            Instantiate(
                bulletPrefab,
                bulletSpawnPoint.position,
                bulletSpawnPoint.rotation
            );
        }
    }
}
