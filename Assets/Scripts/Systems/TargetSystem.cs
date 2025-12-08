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
        Target.OnTargetSelected += TurnOffTargets;
    }
    private void GetTargets(int startRange, int endRange)
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().AvailableTarget = false;
        }
        for (int i = startRange; i < endRange+1; i++)
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
    private GameObject circle;
    private TMP_Text text;

    public bool AvailableTarget
    {
        set { availableTarget = value; }
    }
    private void Start()
    {
        circle = GameObject.Find("Circle"+ gameObject.name);
        circle.SetActive(false);
        text = GameObject.Find("TArgets").GetComponent<TMP_Text>();
    }
    private void OnMouseOver()
    {
        if (!availableTarget)return;
        circle.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (!availableTarget)return;
        circle.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (!availableTarget) return;
        Debug.Log(gameObject.name + " selected");
        circle.GetComponent<SpriteRenderer>().color = Color.red;
        circle.SetActive(true);
        Invoke("changeBack", 0.5f);
        text.text = "Targets Resetted";
        OnTargetSelected?.Invoke(gameObject);
    }
    private void changeBack()
    {
        circle.SetActive(false);
        circle.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
