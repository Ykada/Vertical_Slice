using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class SelectAttack : MonoBehaviour
{
    [SerializeField]private List<GameObject> buttons = new List<GameObject>();
    
    public List<GameObject> Buttons
    {
        get { return buttons; }
    }
    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            button.AddComponent<Attacks>();
        }
        setTargets(0, 0, 2);
        setTargets(1, 0, 1);
        setTargets(2, 1, 2);
    }
    //which attack, 
    private void setTargets(int Attack, int startTarget, int endTarget )
    {
        buttons[Attack].GetComponent<Attacks>().StartTarget = startTarget;
        buttons[Attack].GetComponent<Attacks>().EndTarget = endTarget;
    }
    private void SetStats()
    {

    }
}
public class Attacks : MonoBehaviour
{
    //first target to hit, last target to hit
    public static event Action<float, int, string> Heal;
    public static event Action<float, float, int, string, float> Stats;
    public static event Action<int, int> AttackSelected;
    private int startTarget, endTarget, accuracyAttack;
    private float critAttack, damageMod, debuffChance;
    private string debuffName;
    private bool firstTime = true;

    private void Start()
    {
        
        AttackStats.OnStatChange += ReadStats;
    }
    public float CritAttack
    {
        set { critAttack = value; }
    }
    public float DebuffChance
    {
        set { debuffChance = value; }
    }
    public string DebuffName
    {
        set { debuffName = value; }
    }
    public float DamageMod
    {
        set { damageMod = value; }
    }
    public int AccuracyAttack
    {
        set { accuracyAttack = value; }
    }
    public int StartTarget
    {
        set { startTarget = value; }
    }
    public int EndTarget
    {
        set { endTarget = value; }
    }
    private void OnMouseOver()
    {
        //canvas
        
        gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void OnMouseDown()
    { 
        if (startTarget == 4) 
        {
            Heal?.Invoke(critAttack, ((int)damageMod), debuffName);
            return;
        }
        if (firstTime && startTarget != 0) 
        { 
            AttackSelected?.Invoke(0, endTarget);
        }
        AttackSelected?.Invoke(startTarget, endTarget);
        firstTime = false;
        //attackSelected
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Stats?.Invoke(critAttack, damageMod, accuracyAttack, debuffName, debuffChance);
    }
    private void ReadStats()
    {
        Debug.Log($"{gameObject.name} stats: Crit {critAttack}, Debuff {debuffName}:{debuffChance}, Damage {damageMod * 100}, Accuracy {accuracyAttack}, Targets {startTarget} t/m {endTarget}");
    }
}
