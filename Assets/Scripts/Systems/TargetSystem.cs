using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{ 
    private List<GameObject> targets = new List<GameObject>();
    

    private void Start()
    {
        Attacks.AttackSelected += GetTargets;
        Target.OnTargetSelected += TurnOffTargets;
    }
    private void GetTargets(int startRange, int endRange)
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().AvailableTarget = false;
        }
        for (int i = startRange; i < endRange + 1; i++)
        {
            targets.Add(gameObject.GetComponent<PlacingSystem>().EnemyPrefabs[i]);
            targets[i].GetComponent<Target>().AvailableTarget = true;
        }
    }
    private void TurnOffTargets(GameObject doeIkNiksMee)
    {
        foreach(GameObject target in targets)
        {
            target.GetComponent<Target>().AvailableTarget = false;
        }
    }
}
public class Target : TargetSystem
{
    public static event Action<GameObject> OnTargetSelected;
    private bool availableTarget = false;

    public bool AvailableTarget
    {
        set { availableTarget = value; }
    }
    private void Start()
    {

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
        OnTargetSelected?.Invoke(gameObject);
    }
}
