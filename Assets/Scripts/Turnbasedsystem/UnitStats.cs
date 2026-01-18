using UnityEngine;

public class UnitStats : BaseUnit
{
    [SerializeField] private string characterName;

    public int Speed
    {
        get { return speed; }
    }
    public string CharacterName
    {
        get { return characterName; }
    }
    [HideInInspector]
    public float actionValue = 0f;
}
