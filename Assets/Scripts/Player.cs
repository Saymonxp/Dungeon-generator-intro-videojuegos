using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [field:SerializeField]
    public int TotalHealthPoints { get; private set; }
    [field:SerializeField]
    public Image LifeIndicator {get; private set; }
    public int HealthPoints { get; private set; }
    public float DoubleHealthPoints { get; private set; }
    
    private void Start()
    {
        HealthPoints = TotalHealthPoints;
    }
    
    public void TakeHit()
    {
        if(HealthPoints <= 0)
            return;
    
        HealthPoints--;
        Debug.Log("Vidas: " + HealthPoints + " Total vidas: " + TotalHealthPoints + " Porcentaje: " + DoubleHealthPoints);
        UpdateLifeBar();
        if(HealthPoints <= 0)
            gameObject.SetActive(false);
    }

    public void AddLife()
    {
         Debug.Log("Voy a aumentar la vida de " );
        if(HealthPoints + 1 > TotalHealthPoints)
            return;
    
        HealthPoints++;
        UpdateLifeBar();
    }

    void UpdateLifeBar()
    {
        DoubleHealthPoints = (float)HealthPoints / TotalHealthPoints;
        LifeIndicator.fillAmount = DoubleHealthPoints;
    }
}
