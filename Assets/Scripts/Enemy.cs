using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    [field:SerializeField]    
    public int TotalHealthPoints { get;  set; }
    public int HealthPoints { get;  set; }
    [SerializeField] public float speed = 1;

    public RoomsOnTriggerEnter currRoom;
    public CameraController camera;

    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        player = FindObjectOfType<Player>().transform;
        camera = FindObjectOfType<CameraController>();
        // GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        // int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        // transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

}