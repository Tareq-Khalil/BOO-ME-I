using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    public static Training instance;
    public GameObject dialogue;
    public int trainingIndex = 0;

    [Header("Tower Placement")]
    public TowerPlacementController towerPlacement;
    public GameObject towerPlacementUI;

    [Header("Health")]
    public GameObject jar;
    public GameObject healthUI;

    [Header("Eating Bad")]
    public GameObject badJar;
    public GameObject player;

    [Header("Enemy")]
    public GameObject towers;
    public GameObject enemy;
    public GameObject enemyPrefab;

    public void UpdateTrainingIndex()
    {
        trainingIndex++;
        if(trainingIndex == 1)
        {
            towerPlacement.isInTraining = true;
            towerPlacementUI.SetActive(true);
        }else if (trainingIndex == 2)
        {
            jar.GetComponent<JamPickUp>().isInTraining = true;
            Health.instance.TakeDamage(1);
            healthUI.SetActive(true);
            jar.SetActive(true);
        }
        else if (trainingIndex == 3)
        {
            player.GetComponent<PlayerController>().enabled = false;
            badJar.gameObject.SetActive(true);
            StartCoroutine(PlayerEating());
        }else if (trainingIndex == 4)
        {
            player.GetComponent<Animation>().Play("PlayerAnim");
            StartCoroutine(WaitForAnimation());
        }else if (trainingIndex == 5)
        {
            towers.SetActive(true);
            player.GetComponent<PlayerController>().ShowJar();
            player.GetComponent<PlayerController>().isInTraining = true;
            enemy.GetComponent<MirrorEnemyController>().isInTraining = true;
            enemy.GetComponent<MirrorEnemyController>().TrainingEatingTower();

        }else if(trainingIndex == 6)
        {
            SceneManager.LoadScene(1);
        }

    }
    
    public void StopTraining()
    {
        dialogue.GetComponent<Dialogue>().index++;
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().NextLine();
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(4.5f);
        enemyPrefab.gameObject.SetActive(true);

        dialogue.GetComponent<Dialogue>().index++;
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().NextLine();
    }

    IEnumerator PlayerEating()
    {
        yield return new WaitForSeconds(2);
        player.GetComponent<PlayerController>().enabled = true;
        badJar.gameObject.SetActive(false);
        dialogue.GetComponent<Dialogue>().index++;
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().NextLine();

    }

    public void StopTowerTraining()
    {
        towerPlacement.isInTraining=false;
        towerPlacementUI.SetActive(false);
        dialogue.GetComponent<Dialogue>().index++;
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().NextLine();
    }
    public void StopHealthTraining()
    {
        healthUI.SetActive(false);
        dialogue.GetComponent<Dialogue>().index++;
        dialogue.SetActive(true);
        dialogue.GetComponent<Dialogue>().NextLine();
    }

    private void Awake()
    {
        instance = this;
    }
}
