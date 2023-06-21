using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [field:SerializeField]
    public int TotalHealthPoints { get; private set; }
    [field:SerializeField]
    public Image LifeIndicator {get; private set; }
    public int HealthPoints { get; private set; }
    private AudioManager audioManager;
    
    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void TakeHit()
    {
        if(HealthPoints <= 0)
            return;
        audioManager.selectAudio(4, 0.4f);
        HealthPoints--;
        if(HealthPoints <= 0)
            gameObject.SetActive(false);
    }

    public void AddLife()
    {
        if(HealthPoints + 1 > TotalHealthPoints)
            return;
        audioManager.selectAudio(5, 0.4f);
        HealthPoints++;
    }

    void Update()
    {
        LifeIndicator.fillAmount =  HealthPoints /  TotalHealthPoints;
    }
}
