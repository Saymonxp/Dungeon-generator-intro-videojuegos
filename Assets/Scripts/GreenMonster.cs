using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMonster : MonoBehaviour
{
    [SerializeField] int health = 1;
    
    public void TakeDamage() {
        health--;
    }
}
