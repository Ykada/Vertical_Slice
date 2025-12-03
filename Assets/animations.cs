using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{
    [SerializeField] GameObject idle;
    [SerializeField] GameObject attack;

    float timer = 0f;
    float duration = 1f;
    bool isAttacking = false;

    void Start()
    {
        attack.SetActive(false);
        idle.SetActive(true);
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            StartAttack();
        }

        
        if (isAttacking)
        {
            timer += Time.deltaTime;

            
            if (timer >= duration)
            {
                EndAttack();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        timer = 0f;

        attack.SetActive(true);
        idle.SetActive(false);
    }

    void EndAttack()
    {
        isAttacking = false;

        attack.SetActive(false);
        idle.SetActive(true);
    }
}
