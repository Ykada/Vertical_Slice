using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseHero : MonoBehaviour
{   
    //flat stats 
    [SerializeField] protected int maxHp, prot, dodge, spd, accMod;
    private int accModAttack;
    //percentages
    [SerializeField] protected float crit;
    private float critAttack;
    //base damage range
    [SerializeField] protected Vector2 dmgRange;
    private Vector2 dmgRangeAttack;
    //resistances
    protected SortedList<string, float> resNameWithValue = new SortedList<string, float>();
    //dood of niet
    public bool Dead = false;
    protected bool Deathsdoor = false;
    //HpCounter
    private string debuff;
    private float debuffAcc;
    private int currentHp;
    protected SelectAttack selectAttack;
    
    private void Start()
    {
        //testing
        resNameWithValue.Add("move", 40);
        Invoke("AfterStart", 1);
        //bij enemy turn de acc van de attack oproepen

        //Moet naar indivuele hero scripts 
        Target.OnTargetSelected += DealDamage;
        Attacks.Stats += getStats;
        selectAttack = GameObject.Find("SelectAttack").GetComponent<SelectAttack>();
        currentHp = maxHp;
    }
    //testing
    protected void AfterStart()
    {
        
        selectAttack.Buttons[1].GetComponent<Attacks>().DmgMod = 5;
        selectAttack.Buttons[0].GetComponent<Attacks>().DmgMod = 7;
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
        currentHp -= damageEnemy - prot;
        //bools goedzetten
        Deathsdoor = currentHp == 0;
        Dead = currentHp < 0;
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
        if (critAttack >= critCheck)
        {
            dmgRangeAttack = new Vector2(dmgRangeAttack.x * 2, dmgRangeAttack.y * 2);
        }
        int damageAttack = Random.Range(Mathf.CeilToInt(dmgRangeAttack.x), Mathf.CeilToInt(dmgRangeAttack.y));
        target.GetComponent<BaseEnemy>().TakeDamage(accModAttack, damageAttack, debuff, debuffAcc);
        Debug.Log(damageAttack + target.name);
    }
    
    private void getStats(float critAttac, float damage, int accuracyAttack, string debuffName, float debuffChance)
    {
        critAttack = crit + critAttac;
        dmgRangeAttack = new Vector2(dmgRange.x * damage, dmgRange.y * damage);
        accModAttack = accMod + accuracyAttack;
        debuff = debuffName;
        debuffAcc = debuffChance;
        Debug.Log(dmgRangeAttack.x + " " + dmgRangeAttack.y);
    }
}
