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
    [SerializeField] Animator anim;
    bool isDead = false;

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

    private void Update()
    {
        anim.SetBool("Dead", isDead);
        if (HealthPoints <= 0 && !isDead) {
            GameManager.Instance.Kills++;
            Destroy(gameObject, 7f);
            isDead = true;
        }
        if (camera.currRoom == currRoom && HealthPoints > 0)
        {
            Vector2 direction = player.position - transform.position;
            transform.position += (Vector3)direction * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        HealthPoints--;
    }
}