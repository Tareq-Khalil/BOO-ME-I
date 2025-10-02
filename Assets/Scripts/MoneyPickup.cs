using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "MirrorPlayer")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.SetMoney(1);
            Destroy(gameObject);
        }
    }
}
