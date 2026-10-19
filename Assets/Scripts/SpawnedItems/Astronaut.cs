using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [Header("Astronaut")]
    public int scoreValue = 100;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Astronaut hit by: " + other.gameObject.name);

        PawnShip player = other.GetComponentInParent<PawnShip>();

        if (player != null)
        {
            Debug.Log("PLAYER FOUND!");

            if (LevelManager.instance != null)
            {
                Debug.Log("Adding score: " + scoreValue);

                LevelManager.instance.AddScore(scoreValue);
            }

            if (GameManager.instance != null)
            {
                AudioSource.PlayClipAtPoint(
                    GameManager.instance.astronautSound,
                    transform.position,
                    GameManager.instance.SFXVolume
                );
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collider does not belong to PawnShip.");
        }
    }
}