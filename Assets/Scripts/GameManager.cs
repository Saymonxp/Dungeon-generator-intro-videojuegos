using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    
    public int difficulty = 1;
    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    private void AddLife() {
        player.AddLife();
    }
}
