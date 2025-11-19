using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrentTurn : MonoBehaviour
{

    TurnsList turnsListCS;
    // Caracters list
    public List<GameObject> playersInGame = new List<GameObject>();

    private void Start()
    {
        turnsListCS = GetComponent<TurnsList>();



    }
    void randomelist(int count)
    {
        playersInGame = new List<GameObject>(playersInGame);
        for (int i = 0; i < count; i++)
        {
            GameObject temp = playersInGame[i];
            int randomIndex = Random.Range(i, count);
            playersInGame[i] = playersInGame[randomIndex];
            playersInGame[randomIndex] = temp;
        }
    }
}

