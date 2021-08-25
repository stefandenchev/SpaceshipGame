using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManagerScript : MonoBehaviour 
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    Vector3 spawnPosition = new Vector3(0f, 0f, 2.65f);
    float spawnNextEnemyTimeMin = 0.5f;
    float spawnNextEnemyTimeMax = 2f;
    float spawnNextEnemyTime;
    float spawnNextEnemyTimer;
    float coordMinX = -4.3f;
    float coordMaxX = 4.3f;
    private bool isPaused;
    List<GameObject> pool;
    public float PoolSize;

    void Start()
    {
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        GameObject.FindWithTag("Player").GetComponent<PlayerLogicScript>().pauseGame += PauseGame;

        pool = new List<GameObject>();
        for (var i = 0; i < PoolSize; i++)
        {
            pool.Add(Instantiate(enemyPrefab, transform));
            //yield return new WaitForSeconds(0.1f);
            /*if (i >= 5 && i % 5 == 0)
            {
                yield return new WaitForSeconds(0.5f);
            }*/
        }
    }

	void Update ()
    {
        if (isPaused)
        {
            return;
        }

        if (spawnNextEnemyTimer < spawnNextEnemyTime)
        {
            spawnNextEnemyTimer += Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
        }
	}

    void SpawnEnemy()
    {
        spawnPosition.x = Random.Range(coordMinX, coordMaxX);
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        GameObject enemyShip = GetNextFreeItemFromPool();
        if (enemyShip == null)
        {
            return;
        }
        enemyShip.transform.position = spawnPosition;
        enemyShip.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        enemyShip.GetComponent<EnemyScript>().bulletPrefab = enemyBulletPrefab;
        enemyShip.transform.parent = this.transform;
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
    }
    GameObject GetNextFreeItemFromPool()
    {
        int count = pool.Count;
        for (int i = 0; i < count; i++)
        {
            if (!pool[i].activeSelf)
            {
                GameObject item = pool[i];
                item.transform.parent = null;
                item.SetActive(true);
                item.transform.localScale = Vector3.one;
                return item;
            }
        }

        Debug.Log("No free items in the pool");
        return null;
    }
}
