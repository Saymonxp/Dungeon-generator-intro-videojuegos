using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [field:SerializeField]
    public int TotalHealthPoints { get; private set; }
    [field:SerializeField]
    public Image LifeIndicator {get; private set; }
    [field:SerializeField]
    public Text KillsText { get; private set;}
    [field:SerializeField]
    public Text PointsText { get; private set;}
    public int HealthPoints { get; private set; }
    private AudioManager audioManager;
    
    Animator _Animator;
    public float DoubleHealthPoints { get; private set; }
    
    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        audioManager = FindObjectOfType<AudioManager>();
        _Animator = gameObject.GetComponent<Animator>();
    }
    
    public void TakeHit()
    {
        if(HealthPoints <= 0)
            return;
        audioManager.selectAudio(4, 1.5f);
        HealthPoints--;
        UpdateLifeBar();
        if(HealthPoints <= 0)
            _Animator.SetBool("Die", true);
            //gameObject.SetActive(false);
    }

    public void AddLife()
    {
        audioManager.selectAudio(5, 1.5f);
        audioManager.selectAudio(5, 1.5f);
        if(HealthPoints + 1 > TotalHealthPoints)
            GameManager.Instance.Points+=5;
        
        HealthPoints++;
        UpdateLifeBar();
    }

    void Update(){
        KillsText.text = GameManager.Instance.Kills.ToString();
        PointsText.text = GameManager.Instance.Points.ToString();
    }

    void UpdateLifeBar()
    {
        DoubleHealthPoints = (float)HealthPoints / TotalHealthPoints;
        LifeIndicator.fillAmount = DoubleHealthPoints;
        
    }
}
