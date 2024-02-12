using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    
    [SerializeField] GameObject enemy;
    [SerializeField] Vector3 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;


    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = new Vector3(
            UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
            UnityEngine.Random.Range(-spawnArea.y, spawnArea.y),
            UnityEngine.Random.Range(-spawnArea.z, spawnArea.z));

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
        enemyComponent.SetTarget(player);
        enemyComponent.targetDestination.position = player.transform.position;
    }
}
