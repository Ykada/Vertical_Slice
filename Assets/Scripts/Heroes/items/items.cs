using JetBrains.Annotations;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();

    [SerializeField] private GameObject animationbottle;



    void Start()
    {
        animationbottle.SetActive(false);
    }

    public void UseShovel()
    {
        Debug.Log("removed obstacles");
    }

    public void UseTorch()
    {
        Debug.Log("lit torch");
    }

    public void UseHolyWater()
    {
        useHolyWater();
    }

    public void UseAntivenom()
    {
        Debug.Log("anti");
    }

    public void UseBandage()
    {
        Debug.Log("heal");
    }

    public void UseSkeletonKey()
    {
        Debug.Log("used a skeleton key");
    }

    public void UsePortrait()
    {
        Debug.Log("painting");
    }

    public void UseFood()
    {
        Debug.Log("no more hungry");
    }

    void useHolyWater()
    {
        animationbottle.SetActive(true);
        Debug.Log("holy water used");
    }
}
