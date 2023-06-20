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
    public int HealthPoints { get; private set; }
    
    private void Start()
    {
        HealthPoints = TotalHealthPoints;
    }
    
    public void TakeHit()
    {
        if(HealthPoints <= 0)
            return;
    
        HealthPoints--;
        if(HealthPoints <= 0)
            gameObject.SetActive(false);
    }

    public void AddLife()
    {
        if(HealthPoints + 1 > TotalHealthPoints)
            return;
    
        HealthPoints++;
    }

    void Update()
    {
        LifeIndicator.fillAmount =  HealthPoints /  TotalHealthPoints;
        KillsText.text = GameManager.Instance.Kills.ToString();
    }
}
