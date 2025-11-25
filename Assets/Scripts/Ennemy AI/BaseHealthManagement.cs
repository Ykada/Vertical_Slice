using UnityEngine;

// Base class for managing health of enemies

public class BaseHealthManagement : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private string enemyName = "Enemy";
    [SerializeField] private float BaseHealth = 100f;


    public float currentHealth;

    private void Start()
    {
        currentHealth = BaseHealth;


    }

}
