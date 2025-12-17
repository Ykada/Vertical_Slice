using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseHero : MonoBehaviour
{   
    //flat stats 
    [SerializeField] protected int maxHp, prot, dodge, spd, accuracyMod;
    private int accuracyModAttack;
    //percentages
    [SerializeField] protected float crit;
    private float tempCritAttack;
    //base damage range
    [SerializeField] protected Vector2 damageRange;
    private Vector2 damageRangeAttack;
    //resistances
    protected SortedList<string, float> resNameWithValue = new SortedList<string, float>();
    //dood of niet
    private bool Dead = false;
    private bool Deathsdoor = false;
    //HpCounter
    private string debuff;
    private float debuffAccuracy;
    private int currentHp;

    private void Start()
    {
        currentHp = maxHp;
        Target.OnTargetSelected += DealDamage;
        Attacks.Stats += GetStats;
    }
    public void TakeDamage(float accEnemy, int damageEnemy, string debuffName, float debuffAcc)
    {
        //Alleen voor heroes Enemies hebben een dead state
        if (Dead) return;
        //hit berekening
        float hitChance = accEnemy - dodge + 10;
        Debug.Log(hitChance);
        int hitCheck = Random.Range(1, 100);
        if (hitChance >= hitCheck)
        {
            Debug.Log("Dodge");
            return;
        }
        //damage - protection
        currentHp -= (damageEnemy - prot);
        if (currentHp < 0 && !Deathsdoor) currentHp = 0;
        //bools goedzetten
        Deathsdoor = currentHp == 0;
        Dead = currentHp < 0;
        if (Dead) Destroy(gameObject);
        //debuffs
        if (debuffName == null) return;
        int index = resNameWithValue.IndexOfKey(debuffName);
        float debuffHitChance = debuffAcc - resNameWithValue.Values[index];
        Debug.Log(debuffHitChance);
        int debuffCheck = Random.Range(1, 100);
        if (debuffHitChance < debuffCheck) return;
        Debug.Log(debuffName);
        //apply debuff??
    }  
        
    
    protected void HealDamage(int heal)
    {
        if (Dead) return;
        if (currentHp >= maxHp) return;
        //moet nog crit dmg berekenen
        currentHp += heal;
        if (currentHp > maxHp) currentHp = maxHp;
    }

    protected void DealDamage(GameObject target)
    {
        if (Dead) return;
        int critCheck = Random.Range(1, 100);
        if (tempCritAttack >= critCheck)
        {
            damageRangeAttack = new Vector2(damageRangeAttack.x * 2, damageRangeAttack.y * 2);
        }
        int damageAttack = Random.Range(Mathf.CeilToInt(damageRangeAttack.x), Mathf.CeilToInt(damageRangeAttack.y));
        target.GetComponent<BaseEnemy>().TakeDamage(accuracyModAttack, damageAttack, debuff, debuffAccuracy);
        Debug.Log(accuracyModAttack);
    }
    
    private void GetStats(float critAttack, float damage, int accuracyAttack, string debuffName, float debuffChance)
    {
        tempCritAttack = crit + critAttack;
        damageRangeAttack = new Vector2(damageRange.x * damage, damageRange.y * damage);
        accuracyModAttack = accuracyMod + accuracyAttack;
        debuff = debuffName;
        debuffAccuracy = debuffChance;
    }
}
