using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _shootPoint; //De donde sale la bala
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Cuando da click 
        {
            //Shoot
            GameObject projectile = Instantiate(_projectilePrefab); //Crea el proyectil
            projectile.transform.position = _shootPoint.position;
            projectile.transform.rotation = _shootPoint.rotation;
        }
    }
}
