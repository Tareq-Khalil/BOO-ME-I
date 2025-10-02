using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    public int enemiesAmount;
    public int mediumEnemiesAmount;
    public int largeEnemiesAmount;

    private bool spawnLargeEnemyWithJam = true;
    private bool firstEnemyWithDarkJam = true; 

    public GameObject[] enemiesPrefab;

    private int _dice;


    private void Awake()
    {
        instance = this;
    }

    public void SpawnEnemies(int _currentRound)
    {
        switch (_currentRound)
        {
            case 0:
            enemiesAmount = 10;
                break;

            case 1:
                enemiesAmount = 6;
                mediumEnemiesAmount = 4;
                break;


            case 2:
                enemiesAmount = 8;
                mediumEnemiesAmount = 6;
                largeEnemiesAmount = 1;
                break;


            case 3:
                enemiesAmount = 10;
                mediumEnemiesAmount = 8;
                largeEnemiesAmount = 3;
            break;


            default:
                enemiesAmount += 4;
                mediumEnemiesAmount += 2;
                largeEnemiesAmount += 1;
            break;
        }



        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        GameManager.Instance.SetEnemiesAmount(enemiesAmount + mediumEnemiesAmount + largeEnemiesAmount);
        for (int i = 0; i < enemiesAmount; i++)
        {
            Instantiate(enemiesPrefab[0]);
            yield return new WaitForSeconds(0.5f);
        }



        for (int i = 0; i < mediumEnemiesAmount; i++)
        {
            Instantiate(enemiesPrefab[1]);
            yield return new WaitForSeconds(0.5f);
        }



        for (int i = 0; i < largeEnemiesAmount; i++)
        {
            if (firstEnemyWithDarkJam)
            {
                Instantiate(enemiesPrefab[4]);
                firstEnemyWithDarkJam = false;
                yield return new WaitForSeconds(1f);
                ShowUIForFirstDarkJamEnemy();
                yield break;
            }

            //Roll the dice if it's 2 roll again to see which jam is gonna be on the enemy;
            if(spawnLargeEnemyWithJam == false)
            {
               _dice = Random.Range(0, 3);
                
                if(_dice == 2)
                    spawnLargeEnemyWithJam = true;

            }

            if (spawnLargeEnemyWithJam)
            {
                _dice = Random.Range(0, 2);
                if(_dice == 1)
                {
                    Instantiate(enemiesPrefab[4]);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                Instantiate(enemiesPrefab[3]);
                yield return new WaitForSeconds(0.5f);

                }

            }
            else
            {
                Instantiate(enemiesPrefab[2]);
                yield return new WaitForSeconds(0.5f);
            }    



        }
    }
    void ShowUIForFirstDarkJamEnemy()
    {
        Debug.Log("show ui for jam enemy");
    }
}
