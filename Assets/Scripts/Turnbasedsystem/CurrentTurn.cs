using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTurn : MonoBehaviour
{
    public List<GameObject> playersInGame = new List<GameObject>();
    private List<UnitStats> stats = new List<UnitStats>();

    [SerializeField] private Text Selection1, Selection2, Selection3, Selection4, Selection5;

    private UnitStats currentUnit;
    [SerializeField] private float threshold = 1000f;

    // A list that stores the next 5 units acting
    private List<UnitStats> turnQueue = new List<UnitStats>();

    void Start()
    {
        foreach (var obj in playersInGame)
            stats.Add(obj.GetComponent<UnitStats>());
    }

    void Update()
    {
        // Only fill queue when needed
        if (turnQueue.Count < 5)
            UpdateActionBars();
    }

    // Increase action bars and add units to the queue
    private void UpdateActionBars()
    {
        foreach (UnitStats u in stats)
        {
            u.actionValue += u.speed * Time.deltaTime;

            if (u.actionValue >= threshold && !turnQueue.Contains(u))
            {
                turnQueue.Add(u);
                if (turnQueue.Count == 5)
                {
                    UpdateUI();
                }
            }
        }
    }

    // Updates UI Labels
    private void UpdateUI()
    {
        // Clear UI first
        Selection1.text = turnQueue.Count > 0 ? turnQueue[0].characterName : "";
        Selection2.text = turnQueue.Count > 1 ? turnQueue[1].characterName : "";
        Selection3.text = turnQueue.Count > 2 ? turnQueue[2].characterName : "";
        Selection4.text = turnQueue.Count > 3 ? turnQueue[3].characterName : "";
        Selection5.text = turnQueue.Count > 4 ? turnQueue[4].characterName : "";
    }

    // Called when the player ends their turn
    public void EndTurn()
    {
        if (turnQueue.Count == 0) return;

        // The first unit completed its turn
        UnitStats finished = turnQueue[0];
        finished.actionValue = 0;

        // Remove it and shift list
        turnQueue.RemoveAt(0);

        // Try to refill the queue
        UpdateUI();
    }
}
