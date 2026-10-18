using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damageAmount;
    public bool isInstakill;

    // Check this in the Inspector.
    // TRUE = this object can damage the player.
    // FALSE = this object cannot damage the player.
    public bool damagePlayer;

    private PawnShip owner;

    private void Start()
    {
        // Find the PawnShip that owns this Damager.
        owner = GetComponent<PawnShip>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Get the Health component from whatever we hit.
        Health otherHealth = collision.GetComponent<Health>();

        if (otherHealth == null)
        {
            return;
        }

        // If this Damager cannot damage the player,
        // ignore anything that is the player's PawnShip.
        if (!damagePlayer && collision.GetComponent<PawnShip>() != null)
        {
            return;
        }

        // Don't damage our own PawnShip.
        if (owner != null && collision.GetComponent<PawnShip>() == owner)
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