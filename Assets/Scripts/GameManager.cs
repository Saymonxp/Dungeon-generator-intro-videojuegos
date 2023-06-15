using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Not yet used but may be useful in the future
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }
}
