using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentEnemiesCount;

    public int money = 10;

    public int enemyCount = 0;
    public int maxEnemies = 20;



    [Header("Rounds")]
    public int currentRound = 0;
    public bool roundStarted = false;
    public float timeForNextRound = 7;
    public float timer;


    [Header("UI")]
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI roundTimerText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI needMoreMoneyText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = timeForNextRound;
        roundTimerText.gameObject.SetActive(true);
    }

    
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            roundTimerText.text = "Until next round " + (((int)timer) + 1);

        }
        //Update UI
        if(timer <= 0 && !roundStarted)
        {
            roundStarted = true;
            StartRound();
            roundTimerText.gameObject.SetActive(false);
        }
    }


    void StartRound()
    {
        roundStarted = true;
        WaveSpawner.instance.SpawnEnemies(currentRound);
    }


    public void SetEnemiesAmount(int _amount)
    {
        currentEnemiesCount = _amount;
    }

    public void OnEnemyDeath()
    {
        currentEnemiesCount--;
        if (currentEnemiesCount <= 0)
        {
            timer = timeForNextRound;
            roundStarted = false;
            currentRound++;
            currentRoundText.text = "Round " + (currentRound + 1);
            roundTimerText.gameObject.SetActive(true);
        }
    }

    public void NotEnoughMoney()
    {
        needMoreMoneyText.gameObject.GetComponent<Animation>().Play();
    }

    

    public void SetMoney(int _amount)
    {
        this.GetComponent<AudioSource>().Play();
        money += _amount;
        moneyText.text = money.ToString();
    }

    public void OnButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyCount++;

            if (enemyCount >= maxEnemies)
            {
                RestartGame();
            }
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}
