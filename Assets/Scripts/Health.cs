using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;

    public int health = 7;
    private int currentHealth;

    public float invulnerablityInterval = 1.5f;
    private float interval;


    [Header("UI")]
    public GameObject heartsContent;
    public Sprite heartFull;
    public Sprite heart;
    public GameObject heartPrefab;
    public GameObject gameover;

    [Header("Animations")]
    public Animation damagedAnim;


    private void Awake()
    {
        interval = -100;
        instance = this;
        SetUIHearts();
        currentHealth = health;
    }

    private void Update()
    {
        if(interval > 0)
        {
            interval -= Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        if (interval > 0)
            return;

        interval = invulnerablityInterval;
        currentHealth -= damage;
        damagedAnim.Play();
        SetCurrentHealthUI();
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            gameover.SetActive(true);
            Debug.Log("death");
            //Play animation game over and turn off all the scripts on player
        }
    }
    public void AddHealth(int health)
    {
        if(currentHealth == health) 
            return;
        currentHealth++;
        SetCurrentHealthUI();
    }
    void SetUIHearts()
    {
        for (int i = 0; i < health; i++)
        {
            Instantiate(heartPrefab, heartsContent.transform);
        }
    }
    void SetCurrentHealthUI()
    {
        for (int i = 0; i < health; i++)
        {
            if (i > currentHealth - 1)
            {
                heartsContent.transform.GetChild(i).GetComponent<Image>().sprite = heart;
            }
            else
            {
                heartsContent.transform.GetChild(i).GetComponent<Image>().sprite = heartFull;
            }
        }
    }
}
