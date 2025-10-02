using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int _health;

    private Transform target;
    private int wavePointIndex = 0;

    [Header("Transformation")]
    public bool isTransformed;
    public GameObject fireParticleSystem;


    [Header("Consumables")]
    public float throwForce = 5f;
    public GameObject moneyPrefab;
    [Space]
    public bool isWithJam;
    public GameObject jamPrefab;



    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);


        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, dir.normalized);

        // Apply the rotation only around the Z-axis
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation.eulerAngles.z);


        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextPoint();
        }
    }
    void GetNextPoint ()
    {
        if(wavePointIndex >= Waypoints.points.Length - 1)
        {   
            GameManager.Instance.OnEnemyDeath();
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];

    }
    public void TakeDamage(int _damage)
    {
        _health -= _damage;
        if(_health <= 0)
        {
            GameManager.Instance.OnEnemyDeath();
            ThrowMoney();
            if (isWithJam)
                ThrowJam();
            Destroy(gameObject);
        }
    }

    void ThrowMoney()
    {
        GameObject money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        Rigidbody2D moneyRb = money.GetComponent<Rigidbody2D>();

        // Apply a force in a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        moneyRb.AddForce(randomDirection * throwForce, ForceMode2D.Impulse);
    }

    void ThrowJam()
    {
        GameObject jam = Instantiate(jamPrefab, transform.position, Quaternion.identity);
        Rigidbody2D jamRB = jam.GetComponent<Rigidbody2D>();

        // Apply a force in a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        jamRB.AddForce(randomDirection * throwForce, ForceMode2D.Impulse);
    }
    public void TranformEnemy()
    {
        //Play sound

        GameObject fire = Instantiate(fireParticleSystem,transform);
        isTransformed = true;
    }
}
