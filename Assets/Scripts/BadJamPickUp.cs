using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadJamPickUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().ShowJar();
            Destroy(gameObject);
        }
    }
}
