using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurrentTurn : MonoBehaviour
{
    public List<GameObject> playersInGame = new List<GameObject>();
    private List<UnitStats> stats = new List<UnitStats>();
    private UnitStats currentUnit;

    private float threshold = 1000f;

    void Start()
    {
        foreach (var obj in playersInGame)
            stats.Add(obj.GetComponent<UnitStats>());
    }

    void Update()
    {
        if (currentUnit != null) return;

        foreach (UnitStats u in stats)
        {
            u.actionValue += u.speed * Time.deltaTime;

            if (u.actionValue >= threshold)
            {
                currentUnit = u;
                Debug.Log("Turn: " + u.characterName);
                break;
            }
        }
    }

    public void EndTurn()
    {
        currentUnit.actionValue = 0f;
        currentUnit = null;
    }
}