using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint; //De donde sale la bala

    private AudioManager audioManager;
    
     void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();    
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) // Cuando da espacio 
        {
            //Shoot
            audioManager.selectAudio(3, 0.4f);
            GameObject projectile = Instantiate(_projectilePrefab); //Crea el proyectil
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
        }
    }
}