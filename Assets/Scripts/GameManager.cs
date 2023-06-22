using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    
    public int difficulty = 1;

    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;

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

    private void Start() {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPause){
                Resume();
            } else {
                Pause();
            }
        }
    }

    //Start Menu
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    //Pause Menu
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false; 
    }

    void Pause () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    private void AddLife() {
        player.AddLife();
    }
}
