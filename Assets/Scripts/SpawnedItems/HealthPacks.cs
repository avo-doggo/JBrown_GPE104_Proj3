using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [Header("Health Pack")]
    public float healAmount;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponentInParent<Health>();

        if (health != null)
        {
            health.Heal(healAmount);

            // Pickup sound will go here later.
            AudioSource.PlayClipAtPoint
                (
                    GameManager.instance.healthPackSound,
                    transform.position,
                    GameManager.instance.SFXVolume
                );

            Destroy(gameObject);
        }
    }
}