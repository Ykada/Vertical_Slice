using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

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
        setTargets(0, 0, 1);
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
    public static event Action<float, float, int, string, float> Stats;
    public static event Action<int, int> AttackSelected;
    private int startTarget, endTarget, accAttack;
    private float crit, dmgMod, debuffChance;
    private string debuffName;

    public float Crit
    {
        set { crit = value; }
    }
    public float DebuffChance
    {
        set { debuffChance = value; }
    }
    public string DebuffName
    {
        set { debuffName = value; }
    }
    public float DmgMod
    {
        set { dmgMod = value; }
    }
    public int AccAttack
    {
        set { accAttack = value; }
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
        
    }
    private void OnMouseDown()
    {
        //attackSelected
        AttackSelected?.Invoke(startTarget, endTarget);
        Stats?.Invoke(crit, dmgMod, accAttack, debuffName, debuffChance);
    }
}
