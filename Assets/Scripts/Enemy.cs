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
    [SerializeField] protected Animator anim;
    protected bool isDead = false;

    public RoomsOnTriggerEnter currRoom;
    public CameraController camera;

    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        player = FindObjectOfType<Player>().transform;
        camera = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        anim.SetBool("Dead", isDead);
        if (HealthPoints <= 0 && !isDead) {
            GameManager.Instance.Kills++;
            Destroy(gameObject, GameManager.Instance.corpsesDisappearTime);
            isDead = true;
        }
        if (camera.currRoom == currRoom && !isDead)
        {
            Vector2 direction = player.position - transform.position;
            transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        HealthPoints--;
    }
}