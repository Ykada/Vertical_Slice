using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{ 
    private List<GameObject> targets = new List<GameObject>();
    

    private void Start()
    {
        Attacks.AttackSelected += GetTargets;
    }
    private void GetTargets(int startRange, int endRange)
    {
        targets.AddRange(gameObject.GetComponent<PlacingSystem>().EnemyPrefabs);
        if (targets.Count == endRange)
        {
            endRange = targets.Count -1;
        }
        for (int i = startRange; i < endRange+1; i++)
        {
            targets[i].GetComponent<Target>().AvailableTarget = true;
        }
    }
    protected void TurnOffTargets()
    {
        targets.Clear();
        foreach(GameObject target in targets)
        {
            target.GetComponent<Target>().AvailableTarget = false;
        }
    }
}
public class Target : TargetSystem
{
    public static event Action<GameObject, float, float, int, string, float> OnTargetSelected;
    private bool availableTarget = false;
    private float crit, dmg, chanceDebuff;
    private int acc;
    private string debuff;
    public bool AvailableTarget
    {
        set { availableTarget = value; }
    }
    private void Start()
    {
        Attacks.Stats += Attack;
    }
    private void OnMouseOver()
    {
        if (!availableTarget)return;

    }
    private void OnMouseExit()
    {
        if (!availableTarget)return;

    }
    private void OnMouseDown()
    {
        if (!availableTarget) return;
        Debug.Log(gameObject.name + " selected");
        OnTargetSelected?.Invoke(gameObject,crit,dmg,acc,debuff,chanceDebuff);
        TurnOffTargets();
    }
    private void Attack(float critAttack, float damage, int accuracyAttack, string debuffName, float debuffChance)
    {
        crit = critAttack;
        dmg = damage;
        acc = accuracyAttack;
        debuff = debuffName;
        chanceDebuff = debuffChance;
    }
}
