using UnityEngine;

public class Damager : MonoBehaviour
{
    [Header("Damage")]
    public float damageAmount;
    public bool isInstakill;

    [Header("Who Can This Damage?")]
    public bool damagePlayer;
    public bool damageEnemy;
    public bool damageAsteroid;

    private PawnShip owner;

    private void Start()
    {
        owner = GetComponentInParent<PawnShip>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Health otherHealth = collision.GetComponentInParent<Health>();

        if (otherHealth == null)
        {
            return;
        }

        // Check if we hit a player.
        ControllerPlayer player = collision.GetComponentInParent<ControllerPlayer>();

        if (player != null)
        {
            if (!damagePlayer)
            {
                return;
            }
        }

        // Check if we hit an enemy ship.
        AIController enemy = collision.GetComponentInParent<AIController>();

        if (enemy != null)
        {
            if (!damageEnemy)
            {
                return;
            }
        }

        // Check if we hit an asteroid.
        Asteroids asteroid = collision.GetComponentInParent<Asteroids>();

        if (asteroid != null)
        {
            if (!damageAsteroid)
            {
                return;
            }
        }

        // Don't damage our own ship.
        PawnShip otherShip = collision.GetComponentInParent<PawnShip>();

        if (owner != null && otherShip == owner)
        {
            return;
        }

        // Deal damage.
        if (isInstakill)
        {
            otherHealth.TakeDamage(otherHealth.maxHealth);
        }
        else
        {
            otherHealth.TakeDamage(damageAmount);
        }
    }
}