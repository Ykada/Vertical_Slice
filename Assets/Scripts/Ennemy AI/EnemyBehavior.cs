using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
    public static event Action<GameObject, float, float, int, string, float> OnTargetSelected;
    [SerializeField] private PlacingSystem _placingSystem;
    private void OnEnable()
    {
        int target = Random.Range(0, _placingSystem.HeroPrefabs.Count - 1);
        OnTargetSelected?.Invoke(_placingSystem.HeroPrefabs[target], 1, 1, 95,null,0);
    }
}
