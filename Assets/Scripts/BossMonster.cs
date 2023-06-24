using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Enemy, IDamageable
{
    private AudioManager audioManager;

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Coroutine started");

        yield return new WaitForSeconds(5f);

        Debug.Log("Coroutine resumed after 2.5 seconds");
        if (isDead) {
            Debug.Log("Si está muerto");

            GameManager.Instance.Win();
        }

        // Additional code or actions after the delay

        yield return null;
    }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        anim.SetBool("Dead", isDead);
        if (HealthPoints <= 0 && !isDead) {
            GameManager.Instance.Kills++;
            GameManager.Instance.Points+=8;
            audioManager.selectAudio(7, 1.8f);
            Destroy(gameObject, GameManager.Instance.corpsesDisappearTime);
            isDead = true;
            Debug.Log("Voy a mostrar el secreen");
            StartCoroutine(ExampleCoroutine());
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
            audioManager.selectAudio(6, 2.5f);
            collision.GetComponent<IDamageable>().TakeHit();
        }
    }
    
    public void TakeHit() 
    {
        audioManager.selectAudio(7, 2.5f);
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
