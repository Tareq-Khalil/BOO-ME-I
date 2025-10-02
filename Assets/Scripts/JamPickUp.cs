using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamPickUp : MonoBehaviour
{
    public bool isInTraining;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "MirrorPlayer")
        {
            Health.instance.AddHealth(1);
            if(isInTraining)
                Training.instance.StopHealthTraining();
            Destroy(gameObject);
        }
    }
}
