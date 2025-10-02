using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MirrorEnemyController : MonoBehaviour
{
    public bool needToMove = true;
    public Transform mirrorTarget;
    private Transform currentTarget;
    public float speed = 3f;

    [Header("Tower Eating")]
    public TowerPlacementController placementController;
    public LayerMask towerLayer;
    public GameObject towerLayerBounds;
    public float towerEatingCooldown = 5f;
    private bool needToEat = false;
    private bool eating = false;
    private float cooldown;

    public bool isInTraining = false;
    private bool isTargetJar = false;





    private void Awake()
    {
        currentTarget = mirrorTarget;
        cooldown = towerEatingCooldown;
    }

    void Update()
    {


        if (eating)
            return;

        if (!needToEat)
        {
            if(cooldown >= 0)
                    cooldown -= Time.deltaTime;

            if(cooldown <= 0)
                RollDiceForEatingTower();
        }


        if (Vector3.Distance(transform.position, currentTarget.position) <= 0.05f)
        {
            if (isTargetJar)
            {
                StartCoroutine(StartEatingJar());
                return;
            }

            if (needToEat)
                StartCoroutine(StartEatingTower());
        }
      
        Vector3 dir = currentTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(dir.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if(dir.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void RollDiceForEatingTower()
    {
        int _dice = Random.Range(0, 2);
        if (_dice == 0)
        {
            Debug.Log("Ne rolnulo");
            cooldown = towerEatingCooldown;
            return;
        }


        Collider2D[] colliders = Physics2D.OverlapBoxAll(towerLayerBounds.transform.position, towerLayerBounds.GetComponent<Collider2D>().bounds.size, 0f,towerLayer);
        if(colliders.Length == 0) 
        {
            cooldown = towerEatingCooldown;
            return;
        }
        int _randomTower = Random.Range(0,colliders.Length);
        currentTarget = colliders[_randomTower].transform;
        cooldown = towerEatingCooldown;
        needToEat = true;

    }


    IEnumerator StartEatingTower()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        eating = true;
        Tower tower = currentTarget.GetComponent<Tower>();
        while (eating)
        {
            if (isTargetJar)
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                yield break;
            }

            if(tower.health == 1)
            {
                currentTarget = mirrorTarget;
                tower.TakeDamage(1);
                eating = false;
                needToEat = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                yield break;
            }
            tower.TakeDamage(1);
            this.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StartEatingJar()
    {
        placementController.isMirrorPlayerEating = true;
        Destroy(currentTarget.gameObject);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        JarHealth health = gameObject.transform.GetChild(0).GetComponent<JarHealth>();
        eating = true;

        while (eating)
        {
            if (isInTraining)
            {
                Training.instance.StopTraining();
                isInTraining = false;
            }
            if (!isTargetJar)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                placementController.isMirrorPlayerEating = false;
                yield break;
            }

            if(health.health == 1)
            {

                currentTarget = mirrorTarget;
                health.TakeDamage(1);
                eating = false;
                needToEat = false;
                isTargetJar = false;
                placementController.isMirrorPlayerEating = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                yield break;
            }
            health.TakeDamage(1);
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnSeeingJar(Transform _target)
    {
        currentTarget = _target;
        eating = false;
        needToEat = true;
        isTargetJar = true;
    }

    public void TrainingEatingTower()
    {
        needToEat = true;
        isTargetJar = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(towerLayerBounds.transform.position, towerLayerBounds.GetComponent<Collider2D>().bounds.size, 0f, towerLayer);
        if (colliders.Length == 0)
        {
            cooldown = towerEatingCooldown;
            return;
        }
        int _randomTower = Random.Range(0, colliders.Length);
        currentTarget = colliders[_randomTower].transform;
        cooldown = towerEatingCooldown;
        needToEat = true;

        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}
