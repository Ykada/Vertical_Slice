using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int maxHp, prot, dodge, spd, accMod;
    private int currentHp;
    protected SortedList<string, float> resNameWithValue = new SortedList<string, float>();
    public bool Dead = false;
    protected bool corpse = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        corpse = currentHp == 0;
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
}
