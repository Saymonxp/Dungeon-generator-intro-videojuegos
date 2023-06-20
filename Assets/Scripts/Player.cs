using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [field:SerializeField]
    public int TotalHealthPoints { get; private set; }
    [field:SerializeField]
    public Image LifeIndicator {get; private set; }
    public int HealthPoints { get; private set; }
    
    Animator _Animator;
    
    private void Start()
    {
        HealthPoints = TotalHealthPoints;
        _Animator = gameObject.GetComponent<Animator>();
    }
    
    public void TakeHit()
    {

        HealthPoints--;
        if(HealthPoints <= 0)
            _Animator.SetBool("Die", true);
            //gameObject.SetActive(false);
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
    }
}
