using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int difficulty = 1;
    [SerializeField] int kills;

    public int Kills {
        get => kills;
        set {
            kills = value;
            if(kills % 10 == 0) {
                difficulty++;
            }
        }
    }
    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }
}
