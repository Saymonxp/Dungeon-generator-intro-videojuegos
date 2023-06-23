using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Enemy, IDamageable
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (HealthPoints <= 0) {
            GameManager.Instance.Kills++;
            Destroy(gameObject);
        }
        if (camera.currRoom == currRoom)
        {
            Vector2 direction = player.position - transform.position;
            transform.position += (Vector3)direction * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.selectAudio(6, 1.8f);
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        audioManager.selectAudio(7, 1.8f);
        HealthPoints--;
    }
    
    // float currentSpeed;
    // Rigidbody2D rigidBody;

    // private void Start()
    // {
    //     rigidBody = GetComponent<Rigidbody2D>();
    // }
    // private void Update()
    // {
    //     currentSpeed = rigidBody.velocity.magnitude;
    //     Debug.Log("Boss currentSpeed: " + currentSpeed.ToString());
    //     if (!isDead)
    //     {
    //         anim.SetFloat("CurrentSpeed", (float)currentSpeed);
    //     }
    // }
}
