using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BaseHero : MonoBehaviour
{   
    //flat stats 
    [SerializeField] protected int maxHp, prot, dodge, speed, accuracyMod;
    private int accuracyModAttack;
    //percentages
    [SerializeField] protected float crit, stunRes, blightRes, diseaseRes, moveRes, bleedRes, debuffRes;
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
    private int battleProt;
    private bool firstHeal = true;
    //turn
    private CurrentTurn turnsystem;

    public bool FirstHeal
    {
        set {  firstHeal = value; }
    }
    private void Start()
    {
        resNameWithValue.Add("Stun", stunRes);
        resNameWithValue.Add("Blight", blightRes);
        resNameWithValue.Add("Disease", diseaseRes);
        resNameWithValue.Add("Move", moveRes);
        resNameWithValue.Add("Bleed", bleedRes);
        resNameWithValue.Add("Debuff", debuffRes);
        currentHp = maxHp;
        Target.OnTargetSelected += DealDamage;
        Attacks.Stats += GetStats;
        Attacks.Heal += HealDamage;
        turnsystem = GameObject.FindGameObjectWithTag("Turnsystem").GetComponent<CurrentTurn>();
        battleProt = prot;
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
        currentHp -= (damageEnemy - battleProt);
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
        
    
    protected void HealDamage(float critHeal, int heal, string HealOrBuff)
    {
        if (!firstHeal) return;
        firstHeal = false;
        if (HealOrBuff != "Heal")
        {
            battleProt = 20;
            Debug.Log(battleProt);
            turnsystem.EndTurn();
            return;
        }
        int critCheck = Random.Range(1, 100);
        if (critHeal + crit >= critCheck) heal = heal* 2; 
            currentHp += heal;
        if (currentHp > maxHp) currentHp = maxHp;
        turnsystem.EndTurn();
    }

    protected void DealDamage(GameObject target)
    {
        if (Dead) return;
        int critCheck = Random.Range(1, 100);
        if (tempCritAttack >= critCheck) damageRangeAttack = new Vector2(damageRangeAttack.x * 2, damageRangeAttack.y * 2);
        int damageAttack = Random.Range(Mathf.CeilToInt(damageRangeAttack.x), Mathf.CeilToInt(damageRangeAttack.y));
        target.GetComponent<BaseEnemy>().TakeDamage(accuracyModAttack, damageAttack, debuff, debuffAccuracy);
        turnsystem.EndTurn();
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
