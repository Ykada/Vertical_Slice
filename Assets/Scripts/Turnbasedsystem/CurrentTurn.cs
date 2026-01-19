using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTurn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> playersInGame = new List<GameObject>();
   
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
        BaseUnit.OnUnitDeath += RemoveFromQueue;
        Debug.Log("Initialized Turn Queue System");
        UpdateActionBars();
    }

    private void Update()
    {
        // Only update if queue needs filling
        if (turnQueue.Count < playersInGame.Count)
        {
            UpdateActionBars();
        };
    }

    /* <summary>
     Increases action values and assigns units to the turn queue.
     </summary>*/
    private void UpdateActionBars()
    {
        foreach (UnitStats unit in stats)
        {
            unit.actionValue += unit.Speed;
            bool thresholdReached = unit.actionValue >= threshold;
            bool notAlreadyQueued = !turnQueue.Contains(unit);

            if (thresholdReached && notAlreadyQueued)
            {
                turnQueue.Add(unit);

                // Refresh UI when the queue fills
                if (turnQueue.Count == playersInGame.Count)
                    UpdateUI();
            }
        }
    }

    /* <summary>
    Updates the next 5 turn indicators in UI.
    </summary>*/
    private void UpdateUI()
    {
        for (int i = 0; i < turnLabels.Length; i++)
        {
            if (i < turnQueue.Count)
                { turnLabels[i].text = turnQueue[i].CharacterName; }
            else
                turnLabels[i].text = "";
        }
        if (turnQueue[0].CompareTag("Player"))
        {
            turnQueue[0].GetComponent<AttackStats>().enabled = true;
            return;
        }
        if (turnQueue[0].CompareTag("Enemy"))
        turnQueue[0].GetComponent<EnemyBehavior>().enabled = true;
    }

    /* <summary>
     Called when the current acting unit finishes its turn.
     </summary>*/
    public void EndTurn()
    {
        if (turnQueue.Count == 3)
            return;

        // Reset first unit's action bar
        UnitStats finishedUnit = turnQueue[0];
        finishedUnit.actionValue = 0;
        if (finishedUnit.CompareTag("Player"))
        {
            finishedUnit.GetComponent<AttackStats>().enabled = false;
        }
        else
        {
            finishedUnit.GetComponent<EnemyBehavior>().enabled = false;
        }
        // Remove from queue
        turnQueue.RemoveAt(0);
    }
    private void RemoveFromQueue(GameObject sideOfNoChange, string whatWay, GameObject TargetOfChange)
    {
        playersInGame.Remove(TargetOfChange);
        turnQueue.Remove(TargetOfChange.GetComponent<UnitStats>());
        stats.Remove(TargetOfChange.GetComponent<UnitStats>());
    }
}
