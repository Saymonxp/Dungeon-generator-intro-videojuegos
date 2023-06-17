using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [Range(1, 10)][SerializeField] float spawnRate = 1;

    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true) {
            yield return new WaitForSeconds(1/spawnRate);
            float random = Random.Range(0.0f, 1.0f);

            if (random < GameManager.Instance.difficulty * 0.66f)
            {
                Instantiate(enemyPrefab[0]);
            }
            else {
                Instantiate(enemyPrefab[1]);
            }                
        }
    }
}
