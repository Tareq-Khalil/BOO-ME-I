using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadJamThrowable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MirrorPlayer")
        {
            collision.GetComponent<MirrorEnemyController>().OnSeeingJar(transform);
        }

    }
}
