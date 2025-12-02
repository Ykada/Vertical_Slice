using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class SelectAttack : MonoBehaviour
{
    [SerializeField]private List<GameObject> buttons = new List<GameObject>();
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
}
public class Attacks : MonoBehaviour
{
    //first target to hit, last target to hit
    public static event Action<int, int> AttackSelected;
    private int startTarget, endTarget;

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
        Debug.Log(startTarget + " " + endTarget);
        Debug.Log(gameObject.name);
    }
    private void OnMouseDown()
    {
        //attackSelected
        AttackSelected?.Invoke(startTarget, endTarget);
    }
}
