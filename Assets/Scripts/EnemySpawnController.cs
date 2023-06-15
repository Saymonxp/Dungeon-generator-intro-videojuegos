using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;

    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true) {
            yield return new WaitForSeconds(3);
            float random = Random.Range(0.0f, 1.0f);

            if (random < 0.5f) {
                Instantiate(enemyPrefab[0]);
            }
            else {
                Instantiate(enemyPrefab[1]);
            }                
            Instantiate(enemyPrefab[0]);
        }
    }
}
