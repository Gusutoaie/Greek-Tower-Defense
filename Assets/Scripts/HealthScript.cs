using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHitPoints = 2;  // Use this instead of separate max health fields
    private float currentHitPoints;

    [Header("UI Components")]
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private SpriteRenderer healthFill;

    private void Start()
    {
        currentHitPoints = maxHitPoints;  // Initialize health
        UpdateHealthBar();  // Update the visual representation at start
    }

    public void TakeDamage(float damage)
    {
        currentHitPoints -= damage;
        UpdateHealthBar();  // Update health bar on taking damage

        if (currentHitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currentHitPoints / maxHitPoints;

        healthFill.transform.localScale = new Vector3(healthPercentage, 1, 1); // Scale only in X-direction

    }
}
