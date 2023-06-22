using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Transform player;
    [field:SerializeField]
    
    public int TotalHealthPoints { get; private set; }

    public int HealthPoints { get; private set; }
    [SerializeField] float speed = 1;

    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        player = FindObjectOfType<Player>().transform;
        // GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        // int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        // transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void Update()
    {
        if (HealthPoints <= 0) {
            GameManager.Instance.Kills++;
            Destroy(gameObject);
        }
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction * Time.deltaTime * speed;
    }
    
    public void TakeHit() 
    {
        HealthPoints--;
    }
}