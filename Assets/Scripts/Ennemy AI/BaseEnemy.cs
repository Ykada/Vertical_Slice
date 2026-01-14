using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int maxHp, prot, dodge, spd, accMod;
    private int currentHp;
    protected SortedList<string, float> resNameWithValue = new SortedList<string, float>();
    private bool dead = false;
    private bool corpse = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp= maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float accEnemy, int damageEnemy, string debuffName, float debuffAcc)
    {
        //Alleen voor heroes Enemies hebben een dead state
        if (dead) return;
        //hit berekening
        float hitChance = (dodge - accEnemy) +10;
        if (hitChance < 0) hitChance = 10;
        Debug.Log(hitChance);
        int hitCheck = Random.Range(1, 100);
        if (hitChance >= hitCheck)
        {
            Debug.Log("Dodge");
            return;
        }
        //damage - protection
        currentHp -= damageEnemy - prot;
        if (currentHp < 0 && !corpse)
        { 
            currentHp = 10; 
            corpse = true;
            Debug.Log(corpse);
        }
        //bools goedzetten
        dead = currentHp < 0;
        if (dead) Destroy(gameObject);
        //debuffs
        /*if (debuffName == null) return;
        int index = resNameWithValue.IndexOfKey(debuffName);
        float debuffHitChance = resNameWithValue.Values[index] - debuffAcc;
        Debug.Log(debuffHitChance);
        int debuffCheck = Random.Range(1, 100);
        if (debuffHitChance < debuffCheck) return;
        Debug.Log(debuffName);
        //apply debuff??*/
    }
}
