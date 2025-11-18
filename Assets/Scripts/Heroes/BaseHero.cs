using UnityEngine;

public class BaseHero : MonoBehaviour
{   
    //flat stats 
    [SerializeField] protected int maxHp, prot, dodge, spd, accMod;
    //percentages
    [SerializeField] protected float crit, stunRes, blightRes, diseaseRes, deathBlowRes, moveRes, bleedRes, debuffRes, trapRes;
    //base damage range
    [SerializeField] protected Vector2 dmgRange;

    private float TimeElapsed;
    private void Start()
    {
        //bij enemy turn de acc van de attack oproepen
    }
    private void Update()
    {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed > 2)
        {
            TakeDamage(82.5f);
            TimeElapsed = 0;
        }
    }
    protected void TakeDamage(float accEnemy)
    {
        //hit berekening
        float hitChance = accEnemy - dodge + 10;
        int hitCheck = Random.Range(1, 100); 
        if (hitChance >= hitCheck)
        {
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Dodge");
        }
    }
}
