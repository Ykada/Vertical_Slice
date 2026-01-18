using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackStats : MonoBehaviour
{
    private SelectAttack selectAttack;
    //stats for each attack
    public static event Action OnStatChange;
    [SerializeField] private List<float> damageMods;
    [SerializeField] private List<int> startTargets;
    [SerializeField] private List<int> endTargets;
    [SerializeField] private List<float> critAttacks;
    [SerializeField] private List<string> debuffNames;
    [SerializeField] private List<float> debuffChance;
    [SerializeField] private List<int> accuracyAttacks;

    private void Start()
    {
        selectAttack = GameObject.Find("SelectAttack").GetComponent<SelectAttack>();
        this.enabled = false;
    }
    private void OnEnable()
    {
        if (didStart) SetStats();
    }
    private void SetStats()
    {
        gameObject.GetComponent<BaseUnit>().FirstAction = true; 
        //attack 1
        Attacks temp = selectAttack.Buttons[0].GetComponent<Attacks>();
        temp.DamageMod = damageMods[0];
        temp.StartTarget = startTargets[0];
        temp.EndTarget = endTargets[0];
        temp.CritAttack = critAttacks[0];
        temp.DebuffName = debuffNames[0];
        temp.DebuffChance = debuffChance[0];
        temp.AccuracyAttack = accuracyAttacks[0];
        //attack 2
        temp = selectAttack.Buttons[1].GetComponent<Attacks>();
        temp.DamageMod = damageMods[1];
        temp.StartTarget = startTargets[1];
        temp.EndTarget = endTargets[1];
        temp.CritAttack = critAttacks[1];
        temp.DebuffName = debuffNames[1];
        temp.DebuffChance = debuffChance[1];
        temp.AccuracyAttack = accuracyAttacks[1];
        //attack 3
        temp = selectAttack.Buttons[2].GetComponent<Attacks>();
        temp.DamageMod = damageMods[2];
        temp.StartTarget = startTargets[2];
        temp.EndTarget = endTargets[2];
        temp.CritAttack = critAttacks[2];
        temp.DebuffName = debuffNames[2];
        temp.DebuffChance = debuffChance[2];
        temp.AccuracyAttack = accuracyAttacks[2];
        //attack 4
        temp = selectAttack.Buttons[3].GetComponent<Attacks>();
        temp.DamageMod = damageMods[3];
        temp.StartTarget = startTargets[3];
        temp.EndTarget = endTargets[3];
        temp.CritAttack = critAttacks[3];
        temp.DebuffName = debuffNames[3];
        temp.DebuffChance = debuffChance[3];
        temp.AccuracyAttack = accuracyAttacks[3];
        OnStatChange?.Invoke();
    }
}
