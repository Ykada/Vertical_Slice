using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTurn : MonoBehaviour
{
    [Header("References")]
    public List<GameObject> playersInGame = new List<GameObject>();

    [Header("UI Display (Next 5 Turns)")]
    [SerializeField] private Text[] turnLabels = new Text[5];

    [Header("Settings")]
    [SerializeField] private float threshold = 1000f;

    private List<UnitStats> stats = new List<UnitStats>();
    private List<UnitStats> turnQueue = new List<UnitStats>();

    private void Start()
    {
        foreach (var obj in playersInGame)
        {
            UnitStats unit = obj.GetComponent<UnitStats>();
            if (unit != null)
                stats.Add(unit);
        }

        Debug.Log("Initialized Turn Queue System");
        UpdateActionBars();
    }

    private void Update()
    {

        // Only update if queue needs filling
        if (turnQueue.Count < 5)
        {
            UpdateActionBars();
            addopponenttoceue(stats[1]);
            UpdateUI();
        };
        
        
    }

    /// <summary>
    /// Increases action values and assigns units to the turn queue.
    /// </summary>
    private void UpdateActionBars()
    {
        foreach (UnitStats unit in stats)
        {
            unit.actionValue += unit.speed * Time.deltaTime;

            bool thresholdReached = unit.actionValue >= threshold;
            bool notAlreadyQueued = !turnQueue.Contains(unit);

            if (thresholdReached && notAlreadyQueued)
            {
                turnQueue.Add(unit);

                // Refresh UI when the queue fills
                if (turnQueue.Count == 5)
                    UpdateUI();
            }
        }
    }

    /// <summary>
    /// Updates the next 5 turn indicators in UI.
    /// </summary>
    private void UpdateUI()
    {
        for (int i = 0; i < turnLabels.Length; i++)
        {
            if (i < turnQueue.Count)
                turnLabels[i].text = turnQueue[i].characterName;
            else
                turnLabels[i].text = "";
        }
    }

    /// <summary>
    /// Called when the current acting unit finishes its turn.
    /// </summary>
    public void EndTurn()
    {
        if (turnQueue.Count == 0)
            return;

        // Reset first unit's action bar
        UnitStats finishedUnit = turnQueue[0];
        finishedUnit.actionValue = 0;

        // Remove from queue
        turnQueue.RemoveAt(0);

        // Update display immediately
        UpdateUI();

        addopponenttoceue(stats[1]);
        addopponenttoceue(stats[2]);
        addopponenttoceue(stats[3]);
        addopponenttoceue(stats[4]);
        addopponenttoceue(stats[5]);
    }
    private void addopponenttoceue(UnitStats opponent)
    {
        bool thresholdReached = opponent.actionValue >= threshold;
        bool notAlreadyQueued = !turnQueue.Contains(opponent);

        if (thresholdReached && notAlreadyQueued)
        {
            turnQueue.Add(opponent);

            // Refresh UI when the queue fills
            if (turnQueue.Count == 5)
                UpdateUI();
        }
    }
}
