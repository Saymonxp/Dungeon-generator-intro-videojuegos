using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int difficulty = 1;
    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }
}
