
using System.Collections.Generic;
using UnityEngine;

public class PlacingSystem : MonoBehaviour
{
    //places
    [SerializeField] private List<Transform> placesEnemies = new List<Transform>();
    [SerializeField] private List<Transform> placesHeroes = new List<Transform>();
    //characters
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> heroPrefabs = new List<GameObject>();

    private int indexOfTarget;
    
    public List<GameObject> EnemyPrefabs
    {
        get
        {
            return enemyPrefabs;
        }
    }
    private void Start()
    {
        Placing();
        foreach (GameObject enemy in enemyPrefabs)
        {
            enemy.AddComponent<Target>();
        }
    }
    private void Placing()
    {
        int i = 0;
        foreach (Transform t in placesEnemies)
        {
            enemyPrefabs[i].transform.position = placesEnemies[i].position;
            i++;
            if (i > enemyPrefabs.Count - 1) break;
        }
        i = 0;
        foreach (Transform t in placesHeroes)
        {

            heroPrefabs[i].transform.position = placesHeroes[i].position;
            i++;
            if (i > heroPrefabs.Count - 1) break;
        }
    }
    //caster, what way the debuffed need to go, target of change (dead or because of debuff)
    private void OnPositionChange(GameObject sideOfNoChange, string whatWay, GameObject TargetOfChange)
    {
        if (sideOfNoChange.CompareTag("Enemy"))
        {
            if (!TargetOfChange.activeInHierarchy) 
            { 
                heroPrefabs.Remove(TargetOfChange);
                heroPrefabs.Sort();
                Placing();
                return;
            }
            indexOfTarget = heroPrefabs.IndexOf(TargetOfChange);
            if (whatWay == "left")
            {
                if (indexOfTarget == 0) return;
                MoveLeftorRightHero(-1);
                return;
            }
            else if (indexOfTarget == heroPrefabs.Count - 1) return;
            MoveLeftorRightHero(1);
            return;
        }
        if (!TargetOfChange.activeInHierarchy)
        {
            enemyPrefabs.Remove(TargetOfChange);
            enemyPrefabs.Sort();
            Placing();
            return;
        }
        indexOfTarget = enemyPrefabs.IndexOf(TargetOfChange);
        if (whatWay == "left")
        {
            if (indexOfTarget == 0) return;
            MoveLeftorRightEnemy(-1);
            return;
        }
        else if (indexOfTarget == enemyPrefabs.Count - 1) return;
        MoveLeftorRightEnemy(1);
        return;
    }
    public void MoveLeftorRightHero(int WhatWay)
    {
        GameObject temp = heroPrefabs[indexOfTarget + WhatWay];
        heroPrefabs[indexOfTarget + WhatWay] = heroPrefabs[indexOfTarget];
        heroPrefabs[indexOfTarget] = temp;
        Placing();
    }
    private void MoveLeftorRightEnemy(int WhatWay)
    {
        GameObject temp = enemyPrefabs[indexOfTarget + WhatWay];
        enemyPrefabs[indexOfTarget + WhatWay] = enemyPrefabs[indexOfTarget];
        enemyPrefabs[indexOfTarget] = temp;
        Placing();
    }
}
