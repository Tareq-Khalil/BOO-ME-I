using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarHealth : MonoBehaviour
{

    public int health;

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            health = 10;
            gameObject.SetActive(false);
        }
    }

}
