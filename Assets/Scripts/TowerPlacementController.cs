using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class TowerPlacementController : MonoBehaviour
{   
    public GameObject[] towerPrefab;

    public LayerMask placementLayer;
    public int towerIndex = 0;

    public Transform playerTransform;
    public Transform mirrorEnemyTransform;

    private GameObject towerPreview;

    private int moneyCost;

    public bool isMirrorPlayerEating = false;

    public GameObject currentPlayerTower;

    public bool isInTraining = false;

    

    private void Update()
    {
  /*      if (isTowerSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceTower();
            }
            TowerPreviewPosition();
        }
    */    
    }

    private void TryPlaceTower()
    {
        moneyCost = towerPrefab[towerIndex].GetComponent<Tower>().price;
        if (moneyCost > GameManager.Instance.money)
        {
            GameManager.Instance.NotEnoughMoney();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.zero, 0f, placementLayer);

        if (hit.collider != null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(playerTransform.position, playerTransform.GetComponent<Collider2D>().bounds.size, 0f);


            if (colliders.Length == 2)
            {
                PlaceTower();
                PlaceEnemyTower();
            }
        }
    }


    private void PlaceTower()
    {
        GameObject newTower = Instantiate(towerPrefab[towerIndex], playerTransform.position, Quaternion.identity);
        currentPlayerTower = newTower;
        GameManager.Instance.SetMoney(-moneyCost);
        if(isInTraining)
        {

            Training.instance.StopTowerTraining();
        }
    }
    void PlaceEnemyTower()
    {
        if (isInTraining)
        {
            isMirrorPlayerEating = false;
            return;
        }
        if (isMirrorPlayerEating)
            return;
        GameObject enemyTower = Instantiate(towerPrefab[towerIndex], mirrorEnemyTransform.position, Quaternion.identity);

        enemyTower.GetComponent<Tower>().BecomeEnemyTower(currentPlayerTower);
    }

    public void SetTowerIndex(int _index)
    {
        towerIndex = _index;
      

        TryPlaceTower();


        // towerPreview = Instantiate(towerPrefab[towerIndex]);
       // towerPreview.GetComponent<Collider2D>().enabled = false;
    }
    private void TowerPreviewPosition()
    {
        // If tower preview exists, update its position based on the mouse position
        if (towerPreview != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the tower preview is at the same Z position as the ground

            towerPreview.transform.position = mousePosition;
        }
    }

    private void DestroyTowerPreview()
    {
        // Destroy the tower preview if it exists
        if (towerPreview != null)
        {
            Destroy(towerPreview);
        }
    }
}
