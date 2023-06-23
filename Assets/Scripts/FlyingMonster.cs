using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : Enemy, IDamageable
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
            audioManager.selectAudio(8, 1.0f);
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        audioManager.selectAudio(9, 1.0f);
        HealthPoints--;
    }
}
