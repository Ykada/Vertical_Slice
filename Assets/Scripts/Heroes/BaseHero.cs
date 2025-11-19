using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseHero : MonoBehaviour
{   
    //flat stats 
    [SerializeField] protected int maxHp, prot, dodge, spd, accMod;
    //percentages
    [SerializeField] protected float crit;
    //base damage range
    [SerializeField] protected Vector2 dmgRange;
    //resistances
    protected SortedList<string, float> resNameWithValue = new SortedList<string, float>();
    //dood of niet
    public bool Dead = false;
    protected bool Deathsdoor = false;
    //HpCounter
    private int currentHp;
    //tijdelijk testing
    private float TimeElapsed;
    private void Start()
    {
        //testing
        resNameWithValue.Add("move", 40);
        //bij enemy turn de acc van de attack oproepen
    }
    private void Update()
    {
        //testing
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed > 2)
        {
            TakeDamage(82.5f, 0 , "move", 100);
            TimeElapsed = 0;
        }
    }
    protected void TakeDamage(float accEnemy,int damage ,string debuffName, float debuffAcc)
    {
        //Alleen voor heroes Enemies hebben een dead state
        if (Dead) return;
        //hit berekening
        float hitChance = accEnemy - dodge + 10;
        Debug.Log(hitChance);
        int hitCheck = Random.Range(1, 100); 
        if (hitChance >= hitCheck)
        { 
            //damage - protection
            currentHp -= damage+prot;
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
        else
        {
            Debug.Log("Dodge");
        }
    }
    protected void HealDamage(int heal, Transform target )
    {
        if (Dead) return;
        if (currentHp >= maxHp) return;
        //moet nog crit dmg berekenen
        currentHp += heal;
        if (currentHp > maxHp) currentHp = maxHp;
    }

    protected void DealDamage(float accAttack, int damage, string debuffName, float debuffAcc, GameObject target)
    {
        if (Dead) return;

    }
}
