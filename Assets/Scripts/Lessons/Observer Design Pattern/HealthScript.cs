using UnityEngine;
using System;
using Mono.Cecil;

public class HealthScript: MonoBehaviour
{
    public float currentHealth = 100f;
    public float maximumHealth = 100f;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public AudioClip DamagedAudio;
    public AudioClip DeathAudio;
    

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Debug.Log(amount);
        OnHealthChanged?.Invoke(currentHealth);
        if(DamagedAudio) {
            GameManager.instance.GameSfxSource.clip = DamagedAudio;
            GameManager.instance.GameSfxSource.Play();
        }
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        OnHealthChanged?.Invoke(currentHealth);
        if(currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }

    public void Die()
    {
        OnDeath?.Invoke();
        if(DeathAudio) {
            GameManager.instance.GameSfxSource.clip = DeathAudio;
            GameManager.instance.GameSfxSource.Play();
        }
        if(gameObject.CompareTag("Player"))
        {
            //gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
            
        }
        
    }
}