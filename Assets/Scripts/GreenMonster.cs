using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMonster : Enemy, IDamageable
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        anim.SetBool("Dead", isDead);
        if (HealthPoints <= 0 && !isDead) {
            GameManager.Instance.Kills++;
            audioManager.selectAudio(11, 1.8f);
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
            audioManager.selectAudio(10, 1.0f);
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        audioManager.selectAudio(11, 1.0f);
        HealthPoints--;
    }
}
