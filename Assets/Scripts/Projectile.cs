using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    public bool isEnemyTower = false;

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public void SetPlayerAsEnemy()
    {
        isEnemyTower = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemyTower)
        {
            // If it's an enemy projectile, damage both players and enemies
            Health playerHealth = other.GetComponent<Health>();
            Enemy enemy = other.GetComponent<Enemy>();

            if (playerHealth != null)
            {
                DealDamage(playerHealth);
                Destroy(gameObject); // Destroy the projectile upon hitting a player
            }
            else if (enemy != null && enemy.isTransformed)
            {
                DealDamage(enemy);
                Destroy(gameObject); // Destroy the projectile upon hitting an enemy
            }
        }
        else
        {
            // If it's a player projectile, damage only enemies
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null && !enemy.isTransformed)
            {
                DealDamage(enemy);
                Destroy(gameObject); // Destroy the projectile upon hitting an enemy
            }
        }
    }

    private void DealDamage(Health healthComponent)
    {
        healthComponent.TakeDamage(damage);
    }

    private void DealDamage(Enemy enemyComponent)
    {
        enemyComponent.TakeDamage(damage);
    }
}

