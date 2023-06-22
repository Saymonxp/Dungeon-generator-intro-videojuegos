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
        audioManager.selectAudio(4, 0.4f);

        HealthPoints--;
        Debug.Log("Vidas: " + HealthPoints + " Total vidas: " + TotalHealthPoints + " Porcentaje: " + DoubleHealthPoints);
        UpdateLifeBar();
        if(HealthPoints <= 0)
            _Animator.SetBool("Die", true);
            //gameObject.SetActive(false);
    }

    public void AddLife()
    {
        audioManager.selectAudio(5, 1.0f);
        audioManager.selectAudio(5, 1.0f);
         Debug.Log("Voy a aumentar la vida de " );
        if(HealthPoints + 1 > TotalHealthPoints)
            return;
        
        HealthPoints++;
        UpdateLifeBar();
    }

    void Update(){
        KillsText.text = GameManager.Instance.Kills.ToString();
    }

    void UpdateLifeBar()
    {
        DoubleHealthPoints = (float)HealthPoints / TotalHealthPoints;
        LifeIndicator.fillAmount = DoubleHealthPoints;
        LifeIndicator.fillAmount =  HealthPoints /  TotalHealthPoints;
        
    }
}
