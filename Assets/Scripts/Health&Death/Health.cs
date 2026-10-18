using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth;

    public float maxHealth;

    private Death death;

    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        death = GetComponent<Death>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.fillAmount = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.fillAmount = 1f;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
     //   AudioSource.PlayClipAtPoint(GameManager.instance.damageSound, transform.position, GameManager.instance.SFXVolume);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        //check for health being less than or equal to 0 and call the Die method on the death component
        if (currentHealth <= 0 && death != null)
        {
            death.Die();
        }
    }
}