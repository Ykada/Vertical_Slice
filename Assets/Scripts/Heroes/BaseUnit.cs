using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
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
    private bool dead = false;
    private bool deathsdoor = false;
    private bool corpse = false;
    //HpCounter
    private string debuff;
    private float debuffAccuracy;
    private int currentHp;
    private int battleProt;
    private static bool firstAction = true;
    private static bool dealtDamage = false;
    private int damageAttack;
    //turn
    private CurrentTurn turnsystem;
    public static event Action<GameObject, string, GameObject> OnUnitDeath;
    [SerializeField]private Slider HpSlider;

    public bool DealtDamage
    {
        set { dealtDamage = value; }
    }
    public bool Corpse
    {
        get { return corpse; }
    }
    public bool Dead
    {
        get { return dead; }
    }
    public bool FirstAction
    {
        set {  firstAction = value; }
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
        EnemyBehavior.OnTargetSelected += DealDamage;
        Attacks.Heal += HealDamage;
        turnsystem = GameObject.FindGameObjectWithTag("Turnsystem").GetComponent<CurrentTurn>();
        battleProt = prot;
    }
    public void TakeDamage(float accEnemy, int damageEnemy, string debuffName, float debuffAcc, GameObject caster)
    {
        //Alleen voor heroes Enemies hebben een dead state
        if (Dead) return;
        //hit berekening
        float hitChance = (dodge - accEnemy) + 10;
        if (hitChance <= 0)
        {
            hitChance = 10;
        }
        Debug.Log(hitChance);
        int hitCheck = Random.Range(1, 100);
        if (hitChance >= hitCheck)
        {
            Debug.Log("Dodge");
            return;
        }
        //damage - protection
        currentHp -= (damageEnemy - battleProt);
        if (currentHp < 0 && !deathsdoor && !corpse) currentHp = 0;
        //bools goedzetten
        deathsdoor = currentHp == 0;
        if (gameObject.CompareTag("Enemy") && deathsdoor)
        {
            corpse = true;
            currentHp = 5;
        }
        Debug.Log(currentHp);
        dead = currentHp < 0;
        if (Dead)
        {   
            OnUnitDeath?.Invoke(caster, null, gameObject);
        }
        //debuffs
        /*if (debuffName == null) return;
        int index = resNameWithValue.IndexOfKey(debuffName);
        float debuffHitChance = debuffAcc - resNameWithValue.Values[index];
        Debug.Log(debuffHitChance);
        int debuffCheck = Random.Range(1, 100);
        if (debuffHitChance < debuffCheck) return;*/
        //Debug.Log(debuffName);
        //apply debuff??

        HpSlider.value = (float)currentHp / maxHp;
    }  
        
    
    protected void HealDamage(float critHeal, int heal, string HealOrBuff)
    {
        if (!firstAction) return;
        firstAction = false;
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

    protected void DealDamage(GameObject target, float critAttack, float damage, int accuracyAttack, string debuffName, float debuffChance)
    { 
        if (damageRange.y == 0) return;
        tempCritAttack = crit + critAttack;
        accuracyModAttack = accuracyMod + accuracyAttack;
        debuff = debuffName;
        debuffAccuracy = debuffChance;
        if (!firstAction) return;
        firstAction = false;
        if (Dead) return;
        int critCheck = Random.Range(1, 100);
        Vector2 tempDamageRangeAttack = new Vector2(damageRange.x * damage, damageRange.y * damage);
        if (tempCritAttack >= critCheck) tempDamageRangeAttack = new Vector2(damageRangeAttack.x * 2, damageRangeAttack.y * 2);
        damageAttack = Random.Range(Mathf.CeilToInt(tempDamageRangeAttack.x), Mathf.CeilToInt(tempDamageRangeAttack.y));
        Debug.Log(damageAttack);
        target.GetComponent<BaseUnit>().TakeDamage(accuracyModAttack, damageAttack, debuff, debuffAccuracy, gameObject);
        turnsystem.EndTurn();
    }
}
