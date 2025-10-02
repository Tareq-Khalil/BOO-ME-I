using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower settings")]
    public int damage;
    public float firerate;
    public int numberOfProjectiles;
    public int price;
    public GameObject[] projectilePrefab;

    [Space]
    public float projectileSpeed = 10f;
    public int health = 10;

    [Space]
    public int isEnemyTower = 0;
    public GameObject playerTower;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public void BecomeEnemyTower(GameObject _playerTower)
    {
        playerTower = _playerTower;
        isEnemyTower = 1;
    }

    IEnumerator Shoot()
    {
        while (true)
        {

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = i * 360f / numberOfProjectiles;

                Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

                GameObject projectile = Instantiate(projectilePrefab[isEnemyTower], transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = direction.normalized * projectileSpeed; 


                projectile.GetComponent<Projectile>().SetDamage(damage);
                if(isEnemyTower == 1)
                    projectile.GetComponent<Projectile>().SetPlayerAsEnemy();


                Destroy(projectile, 2f);
            }

            this.GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(firerate);
        }

    }

    public void TakeDamage(int damage)
    {
        if (isEnemyTower == 1)
        {
            playerTower.GetComponent<Tower>().TakeDamage(damage);
        }

        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
