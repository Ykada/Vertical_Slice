using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Items : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseShovel();
        UseTorch();
        UseHolyWater();
        UseAntivenom();
        UseBandage();
        UseSkeletonKey();
        Useportrait();
        UseFood();
    }

    private void UseShovel()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("removed obstacles");
        }
    }

    private void UseTorch()
    {
        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("lit torch");
        }
    }

    private void UseHolyWater()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            Debug.Log("healholywata");
        }
    }

    private void UseAntivenom()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Debug.Log("anti");
        }
    }

    private void UseBandage()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.Log("heal");
        }
    }

    private void UseSkeletonKey()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Debug.Log("used a skeleton key");
        }
    }

    private void Useportrait()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("painting");
        }
    }

    private void UseFood()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("no more hungry");
        }
    }
}
