using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();

    // These functions will be triggered by UI button clicks

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
        Debug.Log("healholywata");
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
}
