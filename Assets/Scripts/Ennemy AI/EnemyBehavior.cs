using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
    public static event Action<GameObject, float, float, int, string, float> OnTargetSelected;
    [SerializeField] private PlacingSystem _placingSystem;
    private GameObject playerTurn;
    private void Start()
    {
        playerTurn = GameObject.Find($"YellowCharacterLine{gameObject.name}");
        playerTurn.SetActive( false );
        this.enabled = false;
    }
    private void OnEnable()
    {
        if (!didStart) return;
        playerTurn.SetActive( true );
        Invoke(nameof(Attack), 2.5f);
    }
    private void Attack()
    {
        gameObject.GetComponent<BaseUnit>().FirstAction = true;
        int target = Random.Range(0, _placingSystem.HeroPrefabs.Count - 1);
        OnTargetSelected?.Invoke(_placingSystem.HeroPrefabs[target], 1, 1, 95, null, 0);
    }
    private void OnDisable()
    {
        playerTurn.SetActive( false );
    }
}
