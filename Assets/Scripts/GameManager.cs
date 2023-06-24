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

    public GameObject gameOverUI;
    public bool isDead; //Pa que solo salga el gameOver una vez

    public GameObject winUI;

    [Range(0, 60)] public int corpsesDisappearTime = 7;
    [SerializeField] int kills = 0;
    [SerializeField] int points = 0;

    public int Kills {
        get => kills;
        set {
            kills = value;
            if(kills % 10 == 0) {
                difficulty++;
            }
        }
    }

     public int Points {
        get => points;
        set {
            points = value;
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
        Time.timeScale = 1f;
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


        //Si muere pierde
        if (player.HealthPoints <= 0  && !isDead){
            isDead = true;
            gameOver();
        }

        //Si mata al boss gana
        
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
    }

    //Game Over Menu
    public void gameOver(){
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    //Win Menu
    public void Win(){
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Coroutine started");

        yield return new WaitForSeconds(2.5f);

        Debug.Log("Coroutine resumed after 2.5 seconds");
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void home(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    //Add Life
    private void AddLife() {
        player.AddLife();
    }


}
