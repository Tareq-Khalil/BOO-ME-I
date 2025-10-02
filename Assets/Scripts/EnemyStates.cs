using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TranformEnemy();
        }
    }
}
